using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("permissions")]
[Route("api/company/permissions")]
[ApiController]
public class CompanyPermissionsController : BaseApiController
{
    private readonly IPermissionService _permissionService;
    public CompanyPermissionsController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet("options")]
    //[HasPermission("permission:read:options")]
    public async Task<ActionResult<SelectListItemResponse<Guid>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _permissionService.GetSelectListAsync(ct));
}
