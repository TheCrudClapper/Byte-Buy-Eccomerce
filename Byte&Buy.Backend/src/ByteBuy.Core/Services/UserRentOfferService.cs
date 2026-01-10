using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.AddressSpecifications;
using static ByteBuy.Core.Specification.RentOfferSpecifications;

namespace ByteBuy.Core.Services;

public class UserRentOfferService : IUserRentOfferService
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IImageService _imageService;
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IItemValidationService _itemValidationService;

    public UserRentOfferService(IItemRepository itemRepository,
        IRentOfferRepository rentOfferRepository,
        IItemValidationService itemValidation,
        IImageService imageService,
        IDeliveryRepository deliveryRepository,
        IPortalUserRepository portalUserRepository)
    {

        _itemRepository = itemRepository;
        _rentOfferRepository = rentOfferRepository;
        _itemValidationService = itemValidation;
        _imageService = imageService;
        _deliveryRepository = deliveryRepository;
        _portalUserRepository = portalUserRepository;
    }
    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, UserRentOfferAddRequest request)
    {
        var validation = await ValidateCountryConditonDelivery(
           request.CategoryId,
           request.ConditionId,
           request.ParcelLockerDeliveries,
           request.OtherDeliveriesIds);

        if (validation.IsFailure)
            return Result.Failure<CreatedResponse>(validation.Error);

        var draftsResult = await SaveImageAndCreateDrafts(request.Images);
        if (draftsResult.IsFailure)
            return Result.Failure<CreatedResponse>(draftsResult.Error);

        var itemCreationResult = Item.CreateUserItem(
           request.Name,
           request.Description,
           request.CategoryId,
           request.ConditionId,
           draftsResult.Value);

        if (itemCreationResult.IsFailure)
        {
            var paths = draftsResult.Value.Select(item => item.ImagePath).ToList();
            _imageService.RollbackImageSave(paths, ImageTypeEnum.Items);
            return Result.Failure<CreatedResponse>(itemCreationResult.Error);
        }

        var item = itemCreationResult.Value;

        var spec = new UserHomeAddressSpec(userId);
        var homeAddress = await _portalUserRepository.GetBySpecAsync(spec);

        if (homeAddress is null)
            return Result.Failure<CreatedResponse>(PortalUserErrors.HomeAddressNotSet);

        var deliveryIds = request.OtherDeliveriesIds
          .Concat(request.ParcelLockerDeliveries ?? []);

        var rentOfferResult = RentOffer.Create(
            item.Id,
            userId,
            request.QuantityAvailable,
            request.PricePerDay,
            request.MaxRentalDays,
            homeAddress,
            deliveryIds);

        if (rentOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(rentOfferResult.Error);

        var rentOffer = rentOfferResult.Value;

        await _itemRepository.AddAsync(item);
        await _rentOfferRepository.AddAsync(rentOffer);
        await _rentOfferRepository.CommitAsync();

        return rentOffer.ToCreatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid userId, Guid id)
    {
        var spec = new UserRentOfferAggregateSpec(userId, id);
        var offer = await _rentOfferRepository.GetBySpecAsync(spec);
        if (offer is null)
            return Result.Failure(OfferErrors.NotFound);

        var item = await _itemRepository.GetAggregateAsync(offer.ItemId);
        if (item is null)
            return Result.Failure(OfferErrors.ItemNotFound);

        offer.Deactivate();
        item.Deactivate();

        await _itemRepository.UpdateAsync(item);
        await _rentOfferRepository.UpdateAsync(offer);
        await _rentOfferRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<UserRentOfferResponse>> GetByIdAsync(Guid userId, Guid id, CancellationToken ct = default)
    {
        var spec = new UserRentOfferToResponseSpec(userId, id);
        var dto = await _rentOfferRepository.GetBySpecAsync(spec);

        return dto is null
            ? Result.Failure<UserRentOfferResponse>(OfferErrors.NotFound)
            : dto;
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid userId, Guid id, UserRentOfferUpdateRequest request)
    {
        var spec = new UserRentOfferAggregateSpec(userId, id);
        var rentOffer = await _rentOfferRepository.GetBySpecAsync(spec);
        if (rentOffer is null)
            return Result.Failure<UpdatedResponse>(OfferErrors.NotFound);

        var item = await _itemRepository.GetAggregateAsync(rentOffer.ItemId);
        if (item is null)
            return Result.Failure<UpdatedResponse>(OfferErrors.ItemNotFound);

        var validation = await ValidateCountryConditonDelivery(
            request.CategoryId,
            request.ConditionId,
            request.ParcelLockerDeliveries,
            request.OtherDeliveriesIds);

        if (validation.IsFailure)
            return Result.Failure<UpdatedResponse>(validation.Error);

        var draftsResult = await SaveImageAndCreateDrafts(request.NewImages);
        if (draftsResult.IsFailure)
            return Result.Failure<UpdatedResponse>(draftsResult.Error);

        var itemUpdateResult = item.UpdateUserItem(
            request.Name,
            request.Description,
            request.CategoryId,
            request.ConditionId,
            draftsResult.Value,
            request.ExistingImages.Select(x => x.ToExistingImageUpdate()));

        if (itemUpdateResult.IsFailure)
        {
            var paths = draftsResult.Value.Select(item => item.ImagePath).ToList();
            _imageService.RollbackImageSave(paths, ImageTypeEnum.Items);
            return Result.Failure<UpdatedResponse>(itemUpdateResult.Error);
        }

        var deliveryIds = request.OtherDeliveriesIds
           .Concat(request.ParcelLockerDeliveries ?? []);

        var offerUpdateResult = rentOffer.Update(
            request.QuantityAvailable,
            request.PricePerDay,
            rentOffer.MaxRentalDays,
            deliveryIds);

        if (offerUpdateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(offerUpdateResult.Error);

        await _itemRepository.UpdateAsync(item);
        await _rentOfferRepository.UpdateAsync(rentOffer);
        await _rentOfferRepository.CommitAsync();

        return rentOffer.ToUpdatedResponse();
    }

    private async Task<Result> ValidateCountryConditonDelivery(Guid categoryId,
        Guid conditionId,
        IEnumerable<Guid>? parcelLockerDeliveries,
        IEnumerable<Guid> otherDeliveries)
    {
        var validation = await _itemValidationService
           .ValidateCategoryAndCondition(categoryId, conditionId);

        if (validation.IsFailure)
            return Result.Failure(validation.Error);

        var validatedDeliveries = await DeliveryValidationHelper.ValidateAllDeliveriesAsync(
           parcelLockerDeliveries,
           otherDeliveries,
           _deliveryRepository);

        if (validatedDeliveries.IsFailure)
            return Result.Failure(validatedDeliveries.Error);

        return Result.Success();
    }

    private async Task<Result<IReadOnlyList<ImageDraft>>> SaveImageAndCreateDrafts(IEnumerable<ImageAddRequest>? newImages)
    {
        if (newImages is null || !newImages.Any())
            return Array.Empty<ImageDraft>();

        var imagesResult = await _imageService.SaveNewImagesAsync(newImages, ImageTypeEnum.Items);
        if (imagesResult.IsFailure)
            return Result.Failure<IReadOnlyList<ImageDraft>>(imagesResult.Error);

        return imagesResult.Value
            .Select(x => x.ToImageDraft())
            .ToList();
    }
}
