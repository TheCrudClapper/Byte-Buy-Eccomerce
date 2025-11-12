using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Role;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IRoleService
{
    public Task<Result<RoleResponse>> AddRole(RoleAddRequest request, CancellationToken ct = default);
    public Task<Result<RoleResponse>> UpdateRole(Guid roleId, RoleUpdateRequest request, CancellationToken ct = default);
    public Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct = default);
    public Task<Result<IEnumerable<RoleResponse>>> GetAllRoles(CancellationToken ct = default);
    public Task<Result<RoleResponse>> GetRole(Guid roleId, CancellationToken ct = default);
    public Task<Result> DeleteRole(Guid roleId, CancellationToken ct = default);
}
