using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Role;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[ApiController]
[Resource("roles")]
[Route("api/company/roles")]
public class CompanyRolesController
    : CrudControllerBase<Guid, RoleAddRequest, RoleUpdateRequest, RoleResponse>
{
    private readonly IRoleService _roleService;
    public CompanyRolesController(IRoleService roleService) : base(roleService)
        => _roleService = roleService;

    [HttpGet("options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
        => HandleResult(await _roleService.GetSelectListAsync(ct));

    [HttpGet("list")]
    public async Task<ActionResult<PagedList<RoleListResponse>>> GetRolesListAsync([FromQuery] RoleListQuery queryParams, CancellationToken ct)
        => HandleResult(await _roleService.GetRolesListAsync(queryParams, ct));
}
