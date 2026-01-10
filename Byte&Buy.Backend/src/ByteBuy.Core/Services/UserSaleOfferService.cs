using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using System.Collections.Generic;
using static ByteBuy.Core.Specification.AddressSpecifications;
using static ByteBuy.Core.Specification.SaleOfferSpecifications;

namespace ByteBuy.Core.Services;

public class UserSaleOfferService : IUserSaleOfferService
{
    private readonly ISaleOfferRepository _saleOfferRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IImageService _imageService;
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IItemValidationService _itemValidationService;
    public UserSaleOfferService(IItemRepository itemRepository,
        ISaleOfferRepository saleOfferRepository,
        IItemValidationService itemValidation,
        IImageService imageService,
        IDeliveryRepository deliveryRepository,
        IPortalUserRepository portalUserRepository)
    {
        _itemRepository = itemRepository;
        _saleOfferRepository = saleOfferRepository;
        _itemValidationService = itemValidation;
        _imageService = imageService;
        _deliveryRepository = deliveryRepository;
        _portalUserRepository = portalUserRepository;
    }

    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, UserSaleOfferAddRequest request)
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

        var saleOfferResult = SaleOffer.Create(
                item.Id,
                userId,
                request.QuantityAvailable,
                request.PricePerItem,
                homeAddress,
                deliveryIds);

        if (saleOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(saleOfferResult.Error);

        var saleOffer = saleOfferResult.Value;

        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.UpdateAsync(saleOffer);
        await _saleOfferRepository.CommitAsync();

        return saleOffer.ToCreatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid userId, Guid id)
    {
        var spec = new UserSaleOfferAggregateSpec(userId, id);
        var offer = await _saleOfferRepository.GetBySpecAsync(spec);
        if (offer is null)
            return Result.Failure(OfferErrors.NotFound);

        var item = await _itemRepository.GetAggregateAsync(offer.ItemId);
        if (item is null)
            return Result.Failure(OfferErrors.NotFound);

        offer.Deactivate();
        item.Deactivate();

        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.UpdateAsync(offer);
        await _saleOfferRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<UserSaleOfferResponse>> GetByIdAsync(Guid userId, Guid id, CancellationToken ct = default)
    {
        var spec = new UserSaleOfferAsResponseDtoSpec(userId, id);
        var offerDto = await _saleOfferRepository.GetBySpecAsync(spec, ct);

        return offerDto is null
            ? Result.Failure<UserSaleOfferResponse>(OfferErrors.NotFound)
            : offerDto;
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid userid, Guid id, UserSaleOfferUpdateRequest request)
    {
        var spec = new SaleOfferWithOfferDeliveriesSpec(id);
        var saleOffer = await _saleOfferRepository.GetBySpecAsync(spec);
        if (saleOffer is null)
            return Result.Failure<UpdatedResponse>(OfferErrors.NotFound);

        var item = await _itemRepository.GetAggregateAsync(saleOffer.ItemId);
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

        var offerUpdateResult = saleOffer.Update(
            request.QuantityAvailable,
            request.PricePerItem,
            deliveryIds);

        if (offerUpdateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(offerUpdateResult.Error);

        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.UpdateAsync(saleOffer);
        await _saleOfferRepository.CommitAsync();

        return saleOffer.ToUpdatedResponse();
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
