using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.SaleOfferSpecifications;

namespace ByteBuy.Core.Services;

public class SaleOfferService : ISaleOfferService
{
    private readonly IItemRepository _itemRepository;
    private readonly ISaleOfferRepository _saleOfferRepository;
    public SaleOfferService(IItemRepository itemRepository, ISaleOfferRepository saleOfferRepository)
    {
        _itemRepository = itemRepository;
        _saleOfferRepository = saleOfferRepository;
    }
    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, SaleOfferAddRequest request)
    {
        if (request.OtherDeliveriesIds is null || !request.OtherDeliveriesIds.Any())
            return Result.Failure<CreatedResponse>(OfferErrors.DeliveryRequired);

        var item = await _itemRepository.GetByIdAsync(request.ItemId);
        if (item is null)
            return Result.Failure<CreatedResponse>(ItemErrors.NotFound);

        var stockUpdateResult = item.SubstractStock(request.QuantityAvailable);
        if (stockUpdateResult.IsFailure)
            return Result.Failure<CreatedResponse>(stockUpdateResult.Error);

        var saleOfferResult = SaleOffer.Create(
            request.ItemId,
            userId,
            request.QuantityAvailable,
            request.PricePerItem);

        if (saleOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(saleOfferResult.Error);

        var saleOffer = saleOfferResult.Value;

        saleOffer
            .AssignDeliveriesToOffer(request.OtherDeliveriesIds);

        await _saleOfferRepository.AddAsync(saleOffer);
        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.CommitAsync();

        return saleOffer.ToCreatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var saleOffer = await _saleOfferRepository.GetByIdAsync(id);
        if (saleOffer is null)
            return Result.Failure(Error.NotFound);

        var item = await _itemRepository.GetByIdAsync(saleOffer.ItemId);
        if (item is null)
            return Result.Failure(Error.NotFound);

        var stockUpdateResult = item.AddStock(saleOffer.QuantityAvailable);
        if (stockUpdateResult.IsFailure)
            return Result.Failure(stockUpdateResult.Error);

        saleOffer.Deactivate();

        await _saleOfferRepository.UpdateAsync(saleOffer);
        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.CommitAsync();
        return Result.Success();
    }

    public async Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var spec = new SaleOfferToSaleOfferResponseSpec(id);
        var saleOfferDto = await _saleOfferRepository.GetBySpecAsync(spec, ct);

        return saleOfferDto is null
            ? Result.Failure<SaleOfferResponse>(Error.NotFound)
            : saleOfferDto;
    }

    public async Task<Result<IReadOnlyCollection<SaleOfferListResponse>>> GetListAsync(CancellationToken ct = default)
    {
        var spec = new SaleOfferToSaleOfferListResponseSpec();
        var saleOfferDtoList = await _saleOfferRepository.GetListBySpecAsync(spec, ct);

        return saleOfferDtoList;
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, SaleOfferUpdateRequest request)
    {
        if (request.OtherDeliveriesIds is null || !request.OtherDeliveriesIds.Any())
            return Result.Failure<UpdatedResponse>(OfferErrors.DeliveryRequired);

        var offer = await _saleOfferRepository.GetAggregateAsync(id);
        if (offer is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var item = await _itemRepository.GetByIdAsync(offer.ItemId);
        if (item is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

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
            request.PricePerItem);

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        var deliveryUpdateResult = offer.UpdateDeliveries(request.OtherDeliveriesIds);
        if (deliveryUpdateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(deliveryUpdateResult.Error);

        await _saleOfferRepository.UpdateAsync(offer);
        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.CommitAsync();

        return offer.ToUpdatedResponse();
    }
}
