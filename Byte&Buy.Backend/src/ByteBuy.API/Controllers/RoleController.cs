using ByteBuy.Core.DTO.Role;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : BaseApiController
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<ActionResult<RoleResponse>> PostRole(RoleAddRequest request, CancellationToken ct)
       => HandleResult(await _roleService.AddRole(request, ct));

    [HttpPut("{roleId}")]
    public async Task<ActionResult<RoleResponse>> PutRole(Guid roleId, RoleUpdateRequest request, CancellationToken ct)
        => HandleResult(await _roleService.UpdateRole(roleId, request, ct));

    [HttpGet("Options")]
    public async Task<ActionResult<IEnumerable<SelectListItem>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _roleService.GetSelectList(ct));

    [HttpGet("{roleId}")]
    public async Task<ActionResult<IEnumerable<SelectListItem>>> GetRole(Guid roleId, CancellationToken ct)
        => HandleResult(await _roleService.GetRole(roleId, ct));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SelectListItem>>> GetRoles(CancellationToken ct)
        => HandleResult(await _roleService.GetAllRoles(ct));

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRole(Guid roleId, CancellationToken ct)
        => HandleResult(await _roleService.DeleteRole(roleId, ct));

}
