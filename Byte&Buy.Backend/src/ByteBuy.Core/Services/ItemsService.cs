using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.ImageStorageContracts;
using ByteBuy.Core.Domain.ImageStorageContracts.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
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

    public async Task<Result<CreatedResponse>> AddCompanyItem(ItemAddRequest request)
    {
        var validateRelatedEntities =
            await CheckCountryAndConditionExists(request.CategoryId, request.ConditionId);

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

        var files = request.Images.Select(i => i.Image).ToList();
        var paths = await _imageStorage.SaveToDirectoryAsync(files, ImageTypeEnum.Items);

        for (int i = 0; i < request.Images.Count; i++)
        {
            var imgReq = request.Images[i];
            var path = paths[i];

            var result = item.AddImage(path, imgReq.AltText);
            if (result.IsFailure)
                return Result.Failure<CreatedResponse>(result.Error);
        }

        await _itemRepository.AddAsync(item);
        await _itemRepository.CommitAsync();

        return item.ToCreatedResponse();
    }

    public Task<Result> DeleteCompanyItem(Guid itemId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ItemResponse>> GetCompanyItem(Guid itemId, CancellationToken ct)
    {
        var itemDto = await _itemRepository.GetBySpecAsync(new CompanyItemToItemResponseDtoSpec(itemId), ct);
        if (itemDto is null)
            return Result.Failure<ItemResponse>(Error.NotFound);

        return itemDto;
    }

    public async Task<Result<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsList(CancellationToken ct)
    {
        return await _itemRepository.GetListBySpecAsync(new CompanyItemsToItemListResponseSpec(), ct);
    }

    public Task<Result<UpdatedResponse>> UpdateCompanyItem(Guid itemId, ItemUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> CheckCountryAndConditionExists(Guid categoryId, Guid conditionId)
    {
        if (!await _categoryRepository.ExistsByCondition(cat => cat.Id == categoryId))
            return Result.Failure(CategoryErrors.NotFound);

        if (!await _conditionRepository.ExistsByCondition(con => con.Id == conditionId))
            return Result.Failure(ConditionErrors.NotFound);

        return Result.Success();
    }
}
