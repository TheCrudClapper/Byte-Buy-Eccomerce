using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPortalUserRepository
{
    Task<IEnumerable<PortalUser>> GetPortalUsersWithRolesAsync(CancellationToken ct = default);
    Task<PortalUser?> GetPortalUserWithAllDataByIdAsync(Guid userId, CancellationToken ct = default);
    Task<PortalUser?> GetPortalUserWithAddress(Guid userId, CancellationToken ct = default);
    Task UpdateAsync(PortalUser portalUser);
}
