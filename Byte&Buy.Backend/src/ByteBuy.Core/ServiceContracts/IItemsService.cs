using ByteBuy.Core.DTO.Public.Item;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IItemsService : IBaseCrudService<Guid, ItemAddRequest, ItemUpdateRequest, ItemResponse>
{
    Task<Result<PagedList<ItemListResponse>>> GetListAsync(ItemListQuery queryParam, CancellationToken ct = default);
}
