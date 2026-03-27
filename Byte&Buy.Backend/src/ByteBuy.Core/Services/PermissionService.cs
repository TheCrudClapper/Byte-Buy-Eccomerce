using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.PermissionSpecifications;

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
        var spec = new PermissionSelectListItemSpec();
        return await _permissionRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
    {
        var permission = await _permissionRepository.GetByNameAsync(permissionName);
        //if permision of given name not found -> deny access
        if (permission is null) return false;

        return await _permissionRepository
            .HasUserOrRolePermissionAsync(userId, permission.Id);
    }

}