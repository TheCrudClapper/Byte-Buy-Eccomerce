using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.SaleOfferSpecifications;

namespace ByteBuy.Core.Services;

public class UserSaleOfferService : IUserSaleOfferService
{
    private readonly ISaleOfferRepository _saleOfferRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IItemValidationService _itemValidationService;
    public UserSaleOfferService(IItemRepository itemRepository,
        ISaleOfferRepository saleOfferRepository,
        IItemValidationService itemValidation)
    {
        _itemRepository = itemRepository;
        _saleOfferRepository = saleOfferRepository;
        _itemValidationService = itemValidation;
    }

    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, UserSaleOfferAddRequest request)
    {
        //var validation = await _itemValidationService
        //    .ValidateCategoryAndCondition(request.CategoryId, request.ConditionId);

        //if (validation.IsFailure)
        //    return Result.Failure<CreatedResponse>(validation.Error);
        throw new NotImplementedException();
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
        throw new NotImplementedException();
        //var validation = await _itemValidationService
        //   .ValidateCategoryAndCondition(request.CategoryId, request.ConditionId);

        //if (validation.IsFailure)
        //    return Result.Failure<UpdatedResponse>(validation.Error);
    }
}
