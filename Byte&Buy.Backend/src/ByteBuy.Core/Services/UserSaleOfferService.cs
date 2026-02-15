using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.AddressSpecifications;
using static ByteBuy.Core.Specification.SaleOfferSpecifications;

namespace ByteBuy.Core.Services;

public class UserSaleOfferService : IUserSaleOfferService
{
    private readonly ISaleOfferRepository _saleOfferRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IItemHelperService _itemHelperService;
    private readonly IUnitOfWork _unitOfWork;

    public UserSaleOfferService(IItemRepository itemRepository,
        ISaleOfferRepository saleOfferRepository,
        IItemHelperService itemValidation,
        IPortalUserRepository portalUserRepository,
        IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _saleOfferRepository = saleOfferRepository;
        _itemHelperService = itemValidation;
        _unitOfWork = unitOfWork;
        _portalUserRepository = portalUserRepository;
    }

    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, UserSaleOfferAddRequest request)
    {
        var validation = await _itemHelperService.ValidateCountryConditonDelivery(
            request.CategoryId,
            request.ConditionId,
            request.ParcelLockerDeliveries,
            request.OtherDeliveriesIds);

        if (validation.IsFailure)
            return Result.Failure<CreatedResponse>(validation.Error);

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

        var spec = new UserHomeAddressSpec(userId);
        var homeAddress = await _portalUserRepository.GetBySpecAsync(spec);

        if (homeAddress is null)
            return Result.Failure<CreatedResponse>(PortalUserErrors.HomeAddressNotSet);

        var deliveryIds = _itemHelperService
            .MergeDeliveryIds(request.OtherDeliveriesIds, request.ParcelLockerDeliveries);

        var seller = Seller.CreatePrivateSeller(userId);

        var saleOfferResult = SaleOffer.Create(
                item.Id,
                userId,
                request.QuantityAvailable,
                request.PricePerItem,
                homeAddress,
                seller,
                deliveryIds);

        if (saleOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(saleOfferResult.Error);

        var saleOffer = saleOfferResult.Value;

        await _itemRepository.AddAsync(item);
        await _saleOfferRepository.AddAsync(saleOffer);

        await _unitOfWork.SaveChangesAsync();
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
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<UserSaleOfferResponse>> GetByIdAsync(Guid userId, Guid id, CancellationToken ct = default)
    {
        var spec = new UserSaleOffeResponseSpec(userId, id);
        var offerDto = await _saleOfferRepository.GetBySpecAsync(spec, ct);

        return offerDto is null
            ? Result.Failure<UserSaleOfferResponse>(OfferErrors.NotFound)
            : offerDto;
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid userid, Guid id, UserSaleOfferUpdateRequest request)
    {
        var spec = new SaleOfferAggregateSpec(id);
        var saleOffer = await _saleOfferRepository.GetBySpecAsync(spec);
        if (saleOffer is null)
            return Result.Failure<UpdatedResponse>(OfferErrors.NotFound);

        var item = await _itemRepository.GetAggregateAsync(saleOffer.ItemId);
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

        var offerUpdateResult = saleOffer.Update(
            request.AdditionalQuantity,
            request.PricePerItem,
            deliveryIds);

        if (offerUpdateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(offerUpdateResult.Error);

        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.UpdateAsync(saleOffer);
        await _unitOfWork.SaveChangesAsync();

        return saleOffer.ToUpdatedResponse();
    }
}
