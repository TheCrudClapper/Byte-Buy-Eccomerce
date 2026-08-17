using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.Filtration.Role;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.API.Controllers.Company;

[ApiController]
[Resource("company-roles")]
[Route("api/company/roles")]
public class CompanyRolesController
    : CrudControllerBase<Guid, RoleAddRequest, RoleUpdateRequest, RoleResponse>
{
    private readonly IRoleService _roleService;
    public CompanyRolesController(IRoleService roleService) : base(roleService)
        => _roleService = roleService;

    [HttpGet("options")]
    [HasPermission("{resource}:read:options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
        => HandleResult(await _roleService.GetSelectListAsync(ct));

    [HttpGet("list")]
    [HasPermission("{resource}:read:many")]
    public async Task<ActionResult<PagedList<RoleListResponse>>> GetRolesListAsync([FromQuery] RoleListQuery queryParams, CancellationToken ct)
        => HandleResult(await _roleService.GetRolesListAsync(queryParams, ct));
}
