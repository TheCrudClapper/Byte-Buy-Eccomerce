using ByteBuy.Core.DTO.Public.Item;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;
using ByteBuy.Services.Filtration;

namespace ByteBuy.Core.ServiceContracts;

public interface IItemsService : IBaseCrudService<Guid, ItemAddRequest, ItemUpdateRequest, ItemResponse>
{
    Task<Result<PagedList<ItemListResponse>>> GetListAsync(ItemListQuery queryParam, CancellationToken ct = default);
}
