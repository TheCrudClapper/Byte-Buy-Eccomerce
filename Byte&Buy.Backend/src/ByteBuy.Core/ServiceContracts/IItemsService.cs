using ByteBuy.Core.DTO.Public.Item;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IItemsService : IBaseCrudService<Guid, ItemAddRequest, ItemUpdateRequest, ItemResponse>
{
    Task<Result<IReadOnlyCollection<ItemListResponse>>> GetCompanyItemsListAsync(CancellationToken ct = default);
}
