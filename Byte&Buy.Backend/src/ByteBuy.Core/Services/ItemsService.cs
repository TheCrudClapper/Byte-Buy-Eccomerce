using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Item;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.ItemsSpecifications;

namespace ByteBuy.Core.Services;

public class ItemsService : IItemsService
{
    private readonly IItemRepository _itemRepository;
    public ItemsService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public Task<Result<CreatedResponse>> AddCompanyItem(ItemAddRequest request)
    {
        throw new NotImplementedException();
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
}
