using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Permission;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPermissionRepository : IRepositoryBase<Permission>
{
    /// <summary>
    /// Check wheter user has access to permission (user explicit grant/deny or role)
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="permissionId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> HasUserOrRolePermissionAsync(Guid userId, Guid permissionId);
    Task<Permission?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<bool> ExistsWithNameAsync(string name, Guid? excludedId = null);
    Task<bool> HasActiveRelations(Guid permissionId);
    Task<IReadOnlyCollection<PermissionResponse>> GetPermissionListAsync(CancellationToken ct = default);
}
