using ByteBuy.Core.Domain.Offers.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.RentOffer;
using ByteBuy.Core.Helpers;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CompanyInfoSpecifications;
using static ByteBuy.Core.Specification.RentOfferSpecifications;
namespace ByteBuy.Core.Services;

public class RentOfferService : IRentOfferService
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly ICompanyRepository _companyInfoRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IItemRepository _itemRepository;
    public RentOfferService(IRentOfferRepository rentOfferRepo,
        IItemRepository itemRepo,
        IDeliveryRepository deliveryRepository,
        ICompanyRepository companyInfoRepository,
        IUnitOfWork unitOfWork)
    {
        _companyInfoRepository = companyInfoRepository;
        _rentOfferRepository = rentOfferRepo;
        _itemRepository = itemRepo;
        _deliveryRepository = deliveryRepository;
        _unitOfWork = unitOfWork;
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

        var spec = new CompanyAddressWithIdSpec();
        var companyData = await _companyInfoRepository.GetBySpecAsync(spec);
        if (companyData is null)
            return Result.Failure<CreatedResponse>(CompanyInfoErrors.NotFound);

        var seller = Seller.CreateCompanySeller(companyData.Id);

        var rentOfferResult = RentOffer.Create(
            request.ItemId,
            userId,
            request.QuantityAvailable,
            request.PricePerDay,
            request.MaxRentalDays,
            companyData.CompanyAddress,
            seller,
            validatedDeliveries.Value.Select(i => i.Id));

        if (rentOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(rentOfferResult.Error);

        var rentOffer = rentOfferResult.Value;

        await _rentOfferRepository.AddAsync(rentOffer);
        await _itemRepository.UpdateAsync(item);

        await _unitOfWork.SaveChangesAsync();

        return rentOffer.ToCreatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var spec = new RentOfferAggregateSpec(id, false);
        var rentOffer = await _rentOfferRepository.GetBySpecAsync(spec);
        if (rentOffer is null)
            return Result.Failure(OfferErrors.NotFound);

        var item = await _itemRepository.GetByIdAsync(rentOffer.ItemId);
        if (item is null)
            return Result.Failure(OfferErrors.ItemNotFound);

        if (rentOffer.QuantityAvailable > 0)
        {
            var stockUpdateResult = item.AddStock(rentOffer.QuantityAvailable);
            if (stockUpdateResult.IsFailure)
                return Result.Failure(stockUpdateResult.Error);
        }

        rentOffer.Deactivate();

        await _rentOfferRepository.UpdateAsync(rentOffer);
        await _itemRepository.UpdateAsync(item);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<RentOfferResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var spec = new RentOfferResponseSpec(id);
        var rentOfferDto = await _rentOfferRepository.GetBySpecAsync(spec, ct);

        return rentOfferDto is null
            ? Result.Failure<RentOfferResponse>(Error.NotFound)
            : rentOfferDto;
    }

    public async Task<Result<PagedList<RentOfferListResponse>>> GetListAsync(
        RentOfferListQuery queryParams, CancellationToken ct = default)
        => await _rentOfferRepository.GetRentOffersListAsync(queryParams, ct);


    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RentOfferUpdateRequest request)
    {
        var spec = new RentOfferAggregateSpec(id);
        var rentOffer = await _rentOfferRepository.GetBySpecAsync(spec);
        if (rentOffer is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var item = await _itemRepository.GetByIdAsync(rentOffer.ItemId);
        if (item is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

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

        var updateResult = rentOffer.Update(
            request.AdditionalQuantity,
            request.PricePerDay,
            rentOffer.MaxRentalDays,
            request.AdditionalRentalDays,
            validatedDeliveries.Value.Select(d => d.Id));

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _rentOfferRepository.UpdateAsync(rentOffer);
        await _itemRepository.UpdateAsync(item);

        await _unitOfWork.SaveChangesAsync();

        return rentOffer.ToUpdatedResponse();
    }
}
