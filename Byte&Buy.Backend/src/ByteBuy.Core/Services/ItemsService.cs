using ByteBuy.Core.Contracts;
using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Abstractions;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Components.Web;
using static ByteBuy.Core.Specification.ItemsSpecifications;

namespace ByteBuy.Core.Services;

public class ItemsService : IItemsService
{
    private readonly IItemRepository _itemRepository;
    private readonly IConditionRepository _conditionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IImageStorage _imageStorage;

    public ItemsService(IItemRepository itemRepository,
        IConditionRepository conditionRepository,
        ICategoryRepository categoryRepository,
        IImageStorage imageStorage)
    {
        _itemRepository = itemRepository;
        _conditionRepository = conditionRepository;
        _categoryRepository = categoryRepository;
        _imageStorage = imageStorage;
    }

    public async Task<Result<CreatedResponse>> AddAsync(ItemAddRequest request)
    {
        var validateRelatedEntities =
            await CheckCountryAndConditionExistsAsync(request.CategoryId, request.ConditionId);

        if (validateRelatedEntities.IsFailure)
            return Result.Failure<CreatedResponse>(validateRelatedEntities.Error);

        var itemCreationResult = Item.CreateCompanyItem(
            request.Name,
            request.Description,
            request.CategoryId,
            request.ConditionId,
            request.StockQuantity);

        if (itemCreationResult.IsFailure)
            return Result.Failure<CreatedResponse>(itemCreationResult.Error);

        var item = itemCreationResult.Value;

        var imagesResult = await HandleNewImagesAsync(request.Images, item);
        if (imagesResult.IsFailure)
            return Result.Failure<CreatedResponse>(imagesResult.Error);

        await _itemRepository.AddAsync(item);
        await _itemRepository.CommitAsync();

        return item.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, ItemUpdateRequest request)
    {
        var aggregate = await _itemRepository.GetAggregateAsync(id);
        if (aggregate is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var validateRelatedEntities =
            await CheckCountryAndConditionExistsAsync(request.CategoryId, request.ConditionId);

        if (validateRelatedEntities.IsFailure)
            return Result.Failure<UpdatedResponse>(validateRelatedEntities.Error);

        var updateResult = aggregate.Update(
            request.Name,
            request.Description,
            request.CategoryId,
            request.ConditionId,
            request.StockQuantity
            );

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        //adding new image
        if (request.NewImages is not null && request.NewImages.Count > 0)
        {
            var imagesResult = await HandleNewImagesAsync(request.NewImages, aggregate);
            if (imagesResult.IsFailure)
                return Result.Failure<UpdatedResponse>(imagesResult.Error);
        }

        //delete from disk
        var imageDeletionResult = DeleteImagesPhysically(request, aggregate);
        if (imageDeletionResult.IsFailure)
            return Result.Failure<UpdatedResponse>(imageDeletionResult.Error);

        //marking as deleted or updating images metadata
        var imageHandlinResult = UpdateOrMarkAsDeletedExistingImages(request, aggregate);
        if (imageHandlinResult.IsFailure)
            return Result.Failure<UpdatedResponse>(imageHandlinResult.Error);


        await _itemRepository.UpdateAsync(aggregate);
        await _itemRepository.CommitAsync();

        return aggregate.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _itemRepository.HasActiveRelationsAsync(id))
            return Result.Failure(ItemErrors.InUse);

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
    {
        return await _itemRepository.GetListBySpecAsync(new CompanyItemsToItemListResponseSpec(), ct);
    }

    // HELPERS //
    private async Task<Result> CheckCountryAndConditionExistsAsync(Guid categoryId, Guid conditionId)
    {
        if (!await _categoryRepository.ExistsByCondition(cat => cat.Id == categoryId))
            return Result.Failure(CategoryErrors.NotFound);

        if (!await _conditionRepository.ExistsByCondition(con => con.Id == conditionId))
            return Result.Failure(ConditionErrors.NotFound);

        return Result.Success();
    }

    private Result DeleteImagesPhysically(ItemUpdateRequest request, Item aggregate)
    {
        var deletedIds = request.ExistingImages
            .Where(i => i.IsDeleted)
            .Select(i => i.Id)
            .ToList();

        var deletedPaths = aggregate.GetImagePathsByIds(deletedIds);

        if (deletedPaths.Count > 0)
        {
            var deletedResult = _imageStorage.DeleteFromDirectory(deletedPaths);
            if (deletedResult.IsFailure)
                return Result.Failure(deletedResult.Error);
        }

        return Result.Success();
    }

    private static Result UpdateOrMarkAsDeletedExistingImages(ItemUpdateRequest request, Item aggregate)
    {
        foreach (var image in request.ExistingImages)
        {
            if (image.IsDeleted)
                aggregate.DeleteImagesById(image.Id);
            else
            {
                var changeResult = aggregate.ChangeImageAltText(image.Id, image.AltText);
                if (changeResult.IsFailure)
                    return Result.Failure(changeResult.Error);
            }
        }

        return Result.Success();
    }

    /// <summary>
    /// Processes and saves a collection of new images for the specified item, associating each imageId with the item
    /// using its provided metadata.
    /// </summary>
    /// <typeparam name="TImageDto">The type of the imageId data transfer object. Must implement the IImageRequestDto interface.</typeparam>
    /// <param name="images">A list of imageId data transfer objects containing the imageId files and associated metadata to be added to the
    /// item. Cannot be null.</param>
    /// <param name="item">The item to which the images will be associated. Cannot be null.</param>
    /// <returns>A Result indicating whether the images were successfully saved and associated with the item. Returns a failure
    /// result if any imageId could not be saved or added.</returns>
    private async Task<Result> HandleNewImagesAsync<TImageDto>(IList<TImageDto> images, Item item)
        where TImageDto : IImageRequestDto
    {

        var files = images.Select(i => i.Image).ToList();

        var imageSaveResult = await _imageStorage.SaveToDirectoryAsync(files, ImageTypeEnum.Items);
        if (imageSaveResult.IsFailure)
            return Result.Failure(imageSaveResult.Error);

        var paths = imageSaveResult.Value;
        for (int i = 0; i < images.Count; i++)
        {
            var imgReq = images[i];
            var path = paths[i];

            var result = item.AddImage(path, imgReq.AltText);
            if (result.IsFailure)
                return Result.Failure(result.Error);
        }

        return Result.Success();
    }

}
