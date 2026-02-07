using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Item;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.ItemsSpecifications;

namespace ByteBuy.Core.Services;

public class ItemsService : IItemsService
{
    private readonly IItemRepository _itemRepository;
    private readonly IItemHelperService _itemHelperService;

    public ItemsService(IItemRepository itemRepository,
        IItemHelperService itemValidationService)
    {
        _itemRepository = itemRepository;
        _itemHelperService = itemValidationService;
    }

    public async Task<Result<CreatedResponse>> AddAsync(ItemAddRequest request)
    {
        var validationResult = await _itemHelperService
            .ValidateCategoryAndCondition(request.CategoryId, request.ConditionId);

        if (validationResult.IsFailure)
            return Result.Failure<CreatedResponse>(validationResult.Error);

        var draftsResult = await _itemHelperService.SaveImageAndCreateDrafts(request.Images);
        if (draftsResult.IsFailure)
            return Result.Failure<CreatedResponse>(draftsResult.Error);

        var itemCreationResult = Item.CreateCompanyItem(
            request.Name,
            request.Description,
            request.CategoryId,
            request.ConditionId,
            request.StockQuantity,
            draftsResult.Value);

        if (itemCreationResult.IsFailure)
        {
            var pathsToRollback = draftsResult.Value.Select(item => item.ImagePath).ToList();
            _itemHelperService.RollbackImageSave(pathsToRollback);
            return Result.Failure<CreatedResponse>(itemCreationResult.Error);
        }

        var item = itemCreationResult.Value;

        await _itemRepository.AddAsync(item);
        await _itemRepository.CommitAsync();

        return item.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, ItemUpdateRequest request)
    {
        var aggregate = await _itemRepository.GetAggregateAsync(id);
        if (aggregate is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var validationResult = await _itemHelperService
            .ValidateCategoryAndCondition(request.CategoryId, request.ConditionId);

        if (validationResult.IsFailure)
            return Result.Failure<UpdatedResponse>(validationResult.Error);

        var draftsResult = await _itemHelperService.SaveImageAndCreateDrafts(request.NewImages);
        if (draftsResult.IsFailure)
            return Result.Failure<UpdatedResponse>(draftsResult.Error);

        var updateResult = aggregate.UpdateCompanyItem(
            request.Name,
            request.Description,
            request.CategoryId,
            request.ConditionId,
            request.AdditionalStockQuantity,
            draftsResult.Value,
            request.ExistingImages
                .Select(i => i.ToExistingImageUpdate()));

        if (updateResult.IsFailure)
        {
            var pathsToRollback = draftsResult.Value.Select(d => d.ImagePath).ToList();
            _itemHelperService.RollbackImageSave(pathsToRollback);
            return Result.Failure<UpdatedResponse>(updateResult.Error);
        }

        //delete from disk
        //var imageDeletionResult = _imageService.DeleteImages(drafts.Select(i => i.ImagePath).ToList(), ImageTypeEnum.Items);
        //if (imageDeletionResult.IsFailure)
        //    return Result.Failure<UpdatedResponse>(imageDeletionResult.Error);

        await _itemRepository.UpdateAsync(aggregate);
        await _itemRepository.CommitAsync();

        return aggregate.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _itemRepository.HasActiveRelationsAsync(id))
            return Result.Failure(ItemErrors.HasActiveOffers);

        var aggregate = await _itemRepository.GetAggregateAsync(id);
        if (aggregate is null)
            return Result.Failure(ItemErrors.NotFound);

        aggregate.Deactivate();

        await _itemRepository.UpdateAsync(aggregate);
        await _itemRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<ItemResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var itemDto = await _itemRepository.GetBySpecAsync(new CompanyItemToItemResponseDtoSpec(id), ct);

        return itemDto is null
            ? Result.Failure<ItemResponse>(Error.NotFound)
            : itemDto;
    }

    public async Task<Result<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsListAsync(CancellationToken ct)
        => await _itemRepository.GetListBySpecAsync(new CompanyItemsToItemListResponseSpec(), ct);
}
