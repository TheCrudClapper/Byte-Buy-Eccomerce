using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid permissionId, CancellationToken ct = default);
    Task<Permission?> GetByNameAsync(string name, CancellationToken ct = default);
    /// <summary>
    /// Check wheter user has access to permission (user explicit grant/deny or role)
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="permissionId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> HasUserOrRolePermissionAsync(Guid userId, Guid permissionId, CancellationToken ct = default);

}
