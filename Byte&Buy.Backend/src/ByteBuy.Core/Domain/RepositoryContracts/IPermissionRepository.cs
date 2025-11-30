using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPermissionRepository
{
    /// <summary>
    /// Check wheter user has access to permission (user explicit grant/deny or role)
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="permissionId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> HasUserOrRolePermissionAsync(Guid userId, Guid permissionId);
    Task<Permission?> GetByIdAsync(Guid permissionId, CancellationToken ct = default);
    Task<Permission?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IEnumerable<Permission>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<RolePermission>> GetAllRolePermissionsAsync(CancellationToken ct = default);
    Task<IEnumerable<Guid>> GetPermissionIdsByRoleIdAsync(Guid roleId, CancellationToken ct = default);
    Task<IEnumerable<Guid>> GetAllPermissionIdsAsync(CancellationToken ct = default);

}
