using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Role;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
namespace ByteBuy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : BaseApiController
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
        => _roleService = roleService;

    [HttpPost]
    //[HasPermission("role:create")]
    public async Task<ActionResult<CreatedResponse>> PostRole(RoleAddRequest request)
       => HandleResult(await _roleService.AddRole(request));

    [HttpPut("{roleId}")]
    //[HasPermission("role:update")]
    public async Task<ActionResult<UpdatedResponse>> PutRole(Guid roleId, RoleUpdateRequest request)
        => HandleResult(await _roleService.UpdateRole(roleId, request));

    [HttpGet("options")]
    //[HasPermission("role:read:options")]
    public async Task<ActionResult<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _roleService.GetSelectList(ct));

    [HttpGet("{roleId}")]
    //[HasPermission("role:read")]
    public async Task<ActionResult<RoleResponse>> GetRole(Guid roleId, CancellationToken ct)
        => HandleResult(await _roleService.GetRole(roleId, ct));

    [HttpGet]
    //[HasPermission("role:read:many")]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> GetRoles(CancellationToken ct)
        => HandleResult(await _roleService.GetAllRoles(ct));

    [HttpDelete("{roleId}")]
    //[HasPermission("role:delete")]
    public async Task<IActionResult> DeleteRole(Guid roleId)
        => HandleResult(await _roleService.DeleteRole(roleId));

}
