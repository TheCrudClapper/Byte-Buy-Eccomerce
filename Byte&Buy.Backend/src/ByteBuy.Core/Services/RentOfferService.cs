using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.RentOffer;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
namespace ByteBuy.Core.Services;

public class RentOfferService : IRentOfferService
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IItemRepository _itemRepository;
    public RentOfferService(IRentOfferRepository rentOfferRepo, IItemRepository itemRepo)
    {
        _rentOfferRepository = rentOfferRepo;
        _itemRepository = itemRepo;
    }
    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, RentOfferAddRequest request)
    {
        var item = await _itemRepository.GetByIdAsync(request.ItemId);
        if (item is null)
            return Result.Failure<CreatedResponse>(ItemErrors.NotFound);

        var stockUpdateResult = item.SubstractStock(request.QuantityAvailable);
        if (stockUpdateResult.IsFailure)
            return Result.Failure<CreatedResponse>(stockUpdateResult.Error);

        var rentOfferResult = RentOffer.Create(
            request.ItemId,
            userId,
            request.QuantityAvailable,
            request.PricePerDay,
            request.MaxRentalDays);

        if (rentOfferResult.IsFailure)
            return Result.Failure<CreatedResponse>(rentOfferResult.Error);

        var rentOffer = rentOfferResult.Value;

        rentOffer
            .AssignDeliveriesToOffer(request.SelectedDeliveriesIds);

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

    public Task<Result<RentOfferResponse>> GetById(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RentOfferUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
