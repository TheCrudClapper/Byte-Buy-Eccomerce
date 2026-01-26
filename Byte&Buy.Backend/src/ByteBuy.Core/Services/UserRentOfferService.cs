using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Shared;
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
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IItemHelperService _itemHelperService;

    public UserRentOfferService(IItemRepository itemRepository,
        IRentOfferRepository rentOfferRepository,
        IItemHelperService itemValidation,
        IPortalUserRepository portalUserRepository)
    {

        _itemRepository = itemRepository;
        _rentOfferRepository = rentOfferRepository;
        _itemHelperService = itemValidation;
        _portalUserRepository = portalUserRepository;
    }
    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, UserRentOfferAddRequest request)
    {
        var validation = await _itemHelperService.ValidateCountryConditonDelivery(
           request.CategoryId,
           request.ConditionId,
           request.ParcelLockerDeliveries,
           request.OtherDeliveriesIds);

        if (validation.IsFailure)
            return Result.Failure<CreatedResponse>(validation.Error);

        var spec = new UserHomeAddressSpec(userId);
        var homeAddress = await _portalUserRepository.GetBySpecAsync(spec);

        if (homeAddress is null)
            return Result.Failure<CreatedResponse>(PortalUserErrors.HomeAddressNotSet);

        var draftsResult = await _itemHelperService.SaveImageAndCreateDrafts(request.Images);
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
            var pathsToRollback = draftsResult.Value.Select(item => item.ImagePath).ToList();
            _itemHelperService.RollbackImageSave(pathsToRollback);
            return Result.Failure<CreatedResponse>(itemCreationResult.Error);
        }

        var item = itemCreationResult.Value;

        var deliveryIds = _itemHelperService
            .MergeDeliveryIds(request.OtherDeliveriesIds, request.ParcelLockerDeliveries);

        var seller = Seller.CreatePrivateSeller(userId);

        var rentOfferResult = RentOffer.Create(
            item.Id,
            userId,
            request.QuantityAvailable,
            request.PricePerDay,
            request.MaxRentalDays,
            homeAddress,
            seller,
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

        var validation = await _itemHelperService.ValidateCountryConditonDelivery(
            request.CategoryId,
            request.ConditionId,
            request.ParcelLockerDeliveries,
            request.OtherDeliveriesIds);

        if (validation.IsFailure)
            return Result.Failure<UpdatedResponse>(validation.Error);

        var draftsResult = await _itemHelperService.SaveImageAndCreateDrafts(request.NewImages);
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
            _itemHelperService.RollbackImageSave(paths);
            return Result.Failure<UpdatedResponse>(itemUpdateResult.Error);
        }

        var deliveryIds = _itemHelperService
            .MergeDeliveryIds(request.OtherDeliveriesIds, request.ParcelLockerDeliveries);

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
}
