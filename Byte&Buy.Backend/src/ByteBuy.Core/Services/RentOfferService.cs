using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.RentOffer;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.RentOfferSpecifications;
namespace ByteBuy.Core.Services;

public class RentOfferService : IRentOfferService
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IItemRepository _itemRepository;
    public RentOfferService(IRentOfferRepository rentOfferRepo, IItemRepository itemRepo,
        IDeliveryRepository deliveryRepository)
    {
        _rentOfferRepository = rentOfferRepo;
        _itemRepository = itemRepo;
        _deliveryRepository = deliveryRepository;
    }
    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, RentOfferAddRequest request)
    {
        var item = await _itemRepository.GetByIdAsync(request.ItemId);
        if (item is null)
            return Result.Failure<CreatedResponse>(ItemErrors.NotFound);

        var validatedDeliveries = await DeliveryValidationHelper.ValidateAllDeliveriesAsync(
           request.ParcelLockerDeliveries,
           request.OtherDeliveriesIds,
           _deliveryRepository);

        if (validatedDeliveries.IsFailure)
            return Result.Failure<CreatedResponse>(validatedDeliveries.Error);

        var stockUpdateResult = item.SubstractStock(request.QuantityAvailable);
        if (stockUpdateResult.IsFailure)
            return Result.Failure<CreatedResponse>(stockUpdateResult.Error);

        var rentOfferResult = RentOffer.Create(
            request.ItemId,
            userId,
            request.QuantityAvailable,
            request.PricePerDay,
            request.MaxRentalDays,
            validatedDeliveries.Value.Select(i => i.Id));

        if (rentOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(rentOfferResult.Error);

        var rentOffer = rentOfferResult.Value;

        await _rentOfferRepository.AddAsync(rentOffer);
        await _itemRepository.UpdateAsync(item);
        await _rentOfferRepository.CommitAsync();

        return rentOffer.ToCreatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var rentOffer = await _rentOfferRepository.GetByIdAsync(id);
        if (rentOffer is null)
            return Result.Failure(Error.NotFound);

        var item = await _itemRepository.GetByIdAsync(rentOffer.ItemId);
        if (item is null)
            return Result.Failure(Error.NotFound);

        var stockUpdateResult = item.AddStock(rentOffer.QuantityAvailable);
        if (stockUpdateResult.IsFailure)
            return Result.Failure(stockUpdateResult.Error);

        rentOffer.Deactivate();

        await _rentOfferRepository.UpdateAsync(rentOffer);
        await _itemRepository.UpdateAsync(item);
        await _rentOfferRepository.CommitAsync();
        return Result.Success();
    }

    public async Task<Result<RentOfferResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var spec = new RentOfferToRentOfferResponseSpec(id);
        var rentOfferDto = await _rentOfferRepository.GetBySpecAsync(spec, ct);

        return rentOfferDto is null
            ? Result.Failure<RentOfferResponse>(Error.NotFound)
            : rentOfferDto;
    }

    public async Task<Result<IReadOnlyCollection<RentOfferListResponse>>> GetListAsync(CancellationToken ct = default)
    {
        var spec = new RentOfferToRentOfferListResponseSpec();
        return await _rentOfferRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RentOfferUpdateRequest request)
    {
        var offer = await _rentOfferRepository.GetAggregateAsync(id);
        if (offer is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var item = await _itemRepository.GetByIdAsync(offer.ItemId);
        if (item is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var validatedDeliveries = await DeliveryValidationHelper.ValidateAllDeliveriesAsync(
           request.ParcelLockerDeliveries,
           request.OtherDeliveriesIds,
           _deliveryRepository);

        if (validatedDeliveries.IsFailure)
            return Result.Failure<UpdatedResponse>(validatedDeliveries.Error);

        var quantityDiff = request.QuantityAvailable - offer.QuantityAvailable;
        if (quantityDiff != 0)
        {
            Result stockUpdateResult;
            if (quantityDiff > 0)
                stockUpdateResult = item.SubstractStock(quantityDiff);
            else
                stockUpdateResult = item.AddStock(-quantityDiff);

            if (stockUpdateResult.IsFailure)
                return Result.Failure<UpdatedResponse>(stockUpdateResult.Error);
        }

        var updateResult = offer.Update(
            request.QuantityAvailable,
            request.PricePerDay,
            request.MaxRentalDays,
            validatedDeliveries.Value.Select(d => d.Id));

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _rentOfferRepository.UpdateAsync(offer);
        await _itemRepository.UpdateAsync(item);
        await _rentOfferRepository.CommitAsync();

        return offer.ToUpdatedResponse();
    }
}
