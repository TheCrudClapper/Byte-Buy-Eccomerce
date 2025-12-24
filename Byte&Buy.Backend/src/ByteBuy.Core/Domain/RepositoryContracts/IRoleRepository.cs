using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRoleRepository
{
    Task<IReadOnlyCollection<ApplicationRole>> GetAllAsync(CancellationToken ct = default);
    Task<ApplicationRole?> GetByIdAsync(Guid roleId, CancellationToken ct = default);
    Task<IReadOnlyCollection<RolePermission>> GetAllRolePermissionsAsync(CancellationToken ct = default);
    Task<bool> ExistsAsync(string roleName, CancellationToken ct = default);
    Task<bool> DoesRoleHaveActiveUsers(Guid roleId);
    Task<IReadOnlyCollection<Guid>> GetPermissionIdsByRoleIdAsync(Guid roleId, CancellationToken ct = default);
    Task<ApplicationRole?> GetAggregateAsync(Guid roleId, CancellationToken ct = default);
}
