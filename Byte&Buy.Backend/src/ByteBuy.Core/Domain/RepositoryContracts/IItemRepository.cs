using ByteBuy.Core.Domain.Items;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Item;
using ByteBuy.Core.Pagination;
using ByteBuy.Services.Filtration;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IItemRepository : IRepositoryBase<Item>
{
    Task<Item?> GetAggregateAsync(Guid itemId, CancellationToken ct = default);
    Task<bool> HasActiveRelationsAsync(Guid itemId);
    Task<PagedList<ItemListResponse>> GetCompanyItemsAsync(ItemListQuery queryParam, CancellationToken ct = default);
}
