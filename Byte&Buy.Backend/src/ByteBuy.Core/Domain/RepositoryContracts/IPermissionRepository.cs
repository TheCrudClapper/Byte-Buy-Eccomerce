using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid permissionId, CancellationToken ct = default);
    Task<Permission?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<bool> CheckUserPermissionGrant(Guid userId, Guid permissionId, CancellationToken ct = default);
    Task<bool> CheckUserPermissionNotGrant(Guid userId, Guid permissionId, CancellationToken ct = default);
    Task<bool> CheckIfRoleHasPermission(Guid roleId, Guid permissionId, CancellationToken ct = default);
    Task<Guid?> GetUserRoleId(Guid userId, CancellationToken ct = default);
}
