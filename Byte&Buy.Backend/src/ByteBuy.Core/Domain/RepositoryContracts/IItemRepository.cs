using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IItemRepository : IRepositoryBase<Item>
{
    Task<Item?> GetAggregateAsync(Guid itemId, CancellationToken ct = default);
}
