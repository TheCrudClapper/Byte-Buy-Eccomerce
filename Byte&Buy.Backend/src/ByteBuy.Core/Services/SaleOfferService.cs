using ByteBuy.Core.Domain.Companies.Errors;
using ByteBuy.Core.Domain.Items.Errors;
using ByteBuy.Core.Domain.Offers;
using ByteBuy.Core.Domain.Offers.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.SaleOffer;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CompanyInfoSpecifications;
using static ByteBuy.Core.Specification.SaleOfferSpecifications;

namespace ByteBuy.Core.Services;

public class SaleOfferService : ISaleOfferService
{
    private readonly IItemRepository _itemRepository;
    private readonly ICompanyRepository _companyInfoRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly ISaleOfferRepository _saleOfferRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaleOfferService(
        IItemRepository itemRepository,
        ISaleOfferRepository saleOfferRepository,
        ICompanyRepository companyInfoRepository,
        IDeliveryRepository deliveryRepository,
        IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _companyInfoRepository = companyInfoRepository;
        _saleOfferRepository = saleOfferRepository;
        _deliveryRepository = deliveryRepository;
        _unitOfWork = unitOfWork;
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

        var spec = new CompanyAddressWithIdSpec();
        var companyData = await _companyInfoRepository.GetBySpecAsync(spec);
        if (companyData is null)
            return Result.Failure<CreatedResponse>(CompanyErrors.NotFound);

        var seller = Seller.CreateCompanySeller(companyData.Id);

        var saleOfferResult = SaleOffer.Create(
            request.ItemId,
            userId,
            request.QuantityAvailable,
            request.PricePerItem,
            companyData.CompanyAddress,
            seller,
            validatedDeliveries.Value.Select(d => d.Id));

        if (saleOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(saleOfferResult.Error);

        var saleOffer = saleOfferResult.Value;

        await _saleOfferRepository.AddAsync(saleOffer);
        await _itemRepository.UpdateAsync(item);

        await _unitOfWork.SaveChangesAsync();

        return saleOffer.ToCreatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var spec = new SaleOfferAggregateSpec(id, false);
        var saleOffer = await _saleOfferRepository.GetBySpecAsync(spec);
        if (saleOffer is null)
            return Result.Failure(Error.NotFound);

        var item = await _itemRepository.GetByIdAsync(saleOffer.ItemId);
        if (item is null)
            return Result.Failure(Error.NotFound);

        if (saleOffer.QuantityAvailable > 0)
        {
            var stockUpdateResult = item.AddStock(saleOffer.QuantityAvailable);
            if (stockUpdateResult.IsFailure)
                return Result.Failure(stockUpdateResult.Error);
        }

        saleOffer.Deactivate();

        await _saleOfferRepository.UpdateAsync(saleOffer);
        await _itemRepository.UpdateAsync(item);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var spec = new SaleOfferResponseSpec(id);
        var saleOfferDto = await _saleOfferRepository.GetBySpecAsync(spec, ct);

        return saleOfferDto is null
            ? Result.Failure<SaleOfferResponse>(Error.NotFound)
            : saleOfferDto;
    }

    public async Task<Result<PagedList<SaleOfferListResponse>>> GetListAsync(SaleOfferListQuery queryParams, CancellationToken ct = default)
       => await _saleOfferRepository.GetSaleOffersListAsync(queryParams, ct);


    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, SaleOfferUpdateRequest request)
    {
        var spec = new SaleOfferAggregateSpec(id);
        var saleOffer = await _saleOfferRepository.GetBySpecAsync(spec);
        if (saleOffer is null)
            return Result.Failure<UpdatedResponse>(OfferErrors.NotFound);

        var item = await _itemRepository.GetByIdAsync(saleOffer.ItemId);
        if (item is null)
            return Result.Failure<UpdatedResponse>(OfferErrors.ItemNotFound);

        var validatedDeliveries = await DeliveryValidationHelper.ValidateAllDeliveriesAsync(
          request.ParcelLockerDeliveries,
          request.OtherDeliveriesIds,
          _deliveryRepository);

        if (validatedDeliveries.IsFailure)
            return Result.Failure<UpdatedResponse>(validatedDeliveries.Error);

        if (request.AdditionalQuantity > 0)
        {
            if (item.StockQuantity < request.AdditionalQuantity)
                return Result.Failure<UpdatedResponse>(ItemErrors.StockNotEnough);
            else
                item.SubstractStock(request.AdditionalQuantity);
        }

        var updateResult = saleOffer.Update(
            request.AdditionalQuantity,
            request.PricePerItem,
            validatedDeliveries.Value.Select(d => d.Id));

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _saleOfferRepository.UpdateAsync(saleOffer);
        await _itemRepository.UpdateAsync(item);
        await _unitOfWork.SaveChangesAsync();

        return saleOffer.ToUpdatedResponse();
    }


}
