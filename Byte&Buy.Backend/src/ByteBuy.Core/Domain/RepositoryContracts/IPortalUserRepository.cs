using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPortalUserRepository
{
    Task<IEnumerable<PortalUser>> GetPortalUSersWithRolesAsync(CancellationToken ct = default);
}
