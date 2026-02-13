using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Role;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Authorize]
[ApiController]
[Resource("roles")]
[Route("api/[controller]")]
public class RolesController
    : CrudControllerBase<Guid, RoleAddRequest, RoleUpdateRequest, RoleResponse>
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService) : base(roleService)
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
