using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Role;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
namespace ByteBuy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController 
    : CrudControllerBase<Guid, RoleAddRequest, RoleUpdateRequest, RoleResponse>
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService) : base(roleService)
        => _roleService = roleService;

    [HttpGet("options")]
    //[HasPermission("role:read:options")]
    public async Task<ActionResult<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _roleService.GetSelectListAsync(ct));

    [HttpGet]
    //[HasPermission("role:read:many")]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> GetRoles(CancellationToken ct)
        => HandleResult(await _roleService.GetAllRolesAsync(ct));
}
