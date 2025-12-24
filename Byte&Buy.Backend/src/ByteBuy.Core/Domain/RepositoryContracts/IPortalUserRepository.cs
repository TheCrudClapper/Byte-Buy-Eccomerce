using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPortalUserRepository : IRepositoryBase<PortalUser>
{
    Task<PortalUser?> GetAggregateAsync(Guid userId, CancellationToken ct = default);
}
