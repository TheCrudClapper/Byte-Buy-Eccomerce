using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CompanyInfoSpecifications;
using static ByteBuy.Core.Specification.SaleOfferSpecifications;

namespace ByteBuy.Core.Services;

public class SaleOfferService : ISaleOfferService
{
    private readonly IItemRepository _itemRepository;
    private readonly ICompanyInfoRepository _companyInfoRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly ISaleOfferRepository _saleOfferRepository;
    public SaleOfferService(
        IItemRepository itemRepository,
        ISaleOfferRepository saleOfferRepository,
        ICompanyInfoRepository companyInfoRepository,
        IDeliveryRepository deliveryRepository)
    {
        _itemRepository = itemRepository;
        _companyInfoRepository = companyInfoRepository;
        _saleOfferRepository = saleOfferRepository;
        _deliveryRepository = deliveryRepository;
    }
    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, SaleOfferAddRequest request)
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

        var spec = new CompanyInfoToAddressValueObject();
        var companyAddress = await _companyInfoRepository.GetBySpecAsync(spec);
        if (companyAddress is null)
            return Result.Failure<CreatedResponse>(ItemErrors.NotFound);

        var saleOfferResult = SaleOffer.Create(
            request.ItemId,
            userId,
            request.QuantityAvailable,
            request.PricePerItem,
            companyAddress,
            validatedDeliveries.Value.Select(d => d.Id));

        if (saleOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(saleOfferResult.Error);

        var saleOffer = saleOfferResult.Value;

        await _saleOfferRepository.AddAsync(saleOffer);
        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.CommitAsync();

        return saleOffer.ToCreatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var spec = new SaleOfferWithOfferDeliveriesSpec(id, false);
        var saleOffer = await _saleOfferRepository.GetBySpecAsync(spec);
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
        return await _saleOfferRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, SaleOfferUpdateRequest request)
    {
        var spec = new SaleOfferWithOfferDeliveriesSpec(id);
        var saleOffer = await _saleOfferRepository.GetBySpecAsync(spec);
        if (saleOffer is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var item = await _itemRepository.GetByIdAsync(saleOffer.ItemId);
        if (item is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var validatedDeliveries = await DeliveryValidationHelper.ValidateAllDeliveriesAsync(
          request.ParcelLockerDeliveries,
          request.OtherDeliveriesIds,
          _deliveryRepository);

        if (validatedDeliveries.IsFailure)
            return Result.Failure<UpdatedResponse>(validatedDeliveries.Error);

        var quantityDiff = request.QuantityAvailable - saleOffer.QuantityAvailable;
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

        var updateResult = saleOffer.Update(
            request.QuantityAvailable,
            request.PricePerItem,
            validatedDeliveries.Value.Select(d => d.Id));

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _saleOfferRepository.UpdateAsync(saleOffer);
        await _itemRepository.UpdateAsync(item);
        await _saleOfferRepository.CommitAsync();

        return saleOffer.ToUpdatedResponse();
    }


}
