using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    public PermissionService(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct = default)
    {
        var permissions = await _permissionRepository.GetAllAsync(ct);
        return permissions.Select(p => p.ToSelectListItemResponse())
            .ToList();
    }

    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
    {
        var permission = await _permissionRepository.GetByNameAsync(permissionName);
        //if permision of given name not found -> deny access
        if (permission == null) return false;

        return await _permissionRepository
            .HasUserOrRolePermissionAsync(userId, permission.Id);
    }

}
