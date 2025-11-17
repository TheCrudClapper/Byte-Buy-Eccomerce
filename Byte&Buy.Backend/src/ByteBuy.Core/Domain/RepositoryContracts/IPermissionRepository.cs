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
    Task<UserPermission?> GetUserPermissionAsync(Guid userId, Guid permissionId, CancellationToken ct = default);
    /// <summary>
    /// Check wheter user has access to permission (user explicit grant/deny or role)
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="permissionId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> HasUserOrRolePermissionAsync(Guid userId, Guid permissionId, CancellationToken ct = default);

}
