using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Role;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IRoleService
{
    public Task<Result<CreatedResponse>> AddRole(RoleAddRequest request);
    public Task<Result<UpdatedResponse>> UpdateRole(Guid roleId, RoleUpdateRequest request);
    public Task<Result> DeleteRole(Guid roleId);
    public Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct = default);
    public Task<Result<IEnumerable<RoleResponse>>> GetAllRoles(CancellationToken ct = default);
    public Task<Result<RoleResponse>> GetRole(Guid roleId, CancellationToken ct = default);
}
