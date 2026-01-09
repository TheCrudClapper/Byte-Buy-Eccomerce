using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.ItemsSpecifications;

namespace ByteBuy.Core.Services;

public class ItemsService : IItemsService
{
    private readonly IItemRepository _itemRepository;
    private readonly IImageService _imageService;
    private readonly IItemValidationService _itemValidationService;

    public ItemsService(IItemRepository itemRepository,
        IItemValidationService itemValidationService,
        IImageService imageService)
    {
        _itemRepository = itemRepository;
        _itemValidationService = itemValidationService;
        _imageService = imageService;
    }

    public async Task<Result<CreatedResponse>> AddAsync(ItemAddRequest request)
    {
        var validationResult = await _itemValidationService
            .ValidateCategoryAndCondition(request.CategoryId, request.ConditionId);

        if (validationResult.IsFailure)
            return Result.Failure<CreatedResponse>(validationResult.Error);

        var imagesResult = await _imageService.SaveNewImagesAsync(request.Images, ImageTypeEnum.Items);
        if (imagesResult.IsFailure)
            return Result.Failure<CreatedResponse>(imagesResult.Error);

        var drafts = imagesResult.Value
            .Select(item => new ImageDraft(item.ImagePath, item.AltText))
            .ToList();

        var itemCreationResult = Item.CreateCompanyItem(
            request.Name,
            request.Description,
            request.CategoryId,
            request.ConditionId,
            request.StockQuantity,
            drafts);

        if (itemCreationResult.IsFailure)
        {
            var paths = drafts.Select(item => item.ImagePath).ToList();
            _imageService.RollbackImageSave(paths, ImageTypeEnum.Items);
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

        var validationResult = await _itemValidationService
            .ValidateCategoryAndCondition(request.CategoryId, request.ConditionId);

        if (validationResult.IsFailure)
            return Result.Failure<UpdatedResponse>(validationResult.Error);

        var imagesResult = await _imageService.SaveNewImagesAsync(request.NewImages, ImageTypeEnum.Items);
        if (imagesResult.IsFailure)
            return Result.Failure<UpdatedResponse>(imagesResult.Error);

        var drafts = imagesResult.Value
            .Select(item => new ImageDraft(item.ImagePath, item.AltText))
            .ToList();

        var updateResult = aggregate.Update(
            request.Name,
            request.Description,
            request.CategoryId,
            request.ConditionId,
            request.StockQuantity,
            drafts,
            request.ExistingImages
                .Select(i => i.ToExistingImageUpdate()));


        if (updateResult.IsFailure)
        {
            var pathsToRollback = drafts.Select(d => d.ImagePath).ToList();
            _imageService.RollbackImageSave(pathsToRollback, ImageTypeEnum.Items);
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
            return Result.Failure(Error.NotFound);

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
