using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Core.ServiceContracts.Base;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-permissions")]
[Route("api/company/permissions")]
[ApiController]
public class CompanyPermissionsController 
    : CrudControllerBase<Guid, PermissionAddRequest, PermissionUpdateRequest, PermissionResponse>
{
    private readonly IPermissionService _permissionService;
    public CompanyPermissionsController(IPermissionService permissionService) : base(permissionService)
        =>  _permissionService = permissionService;

    [HttpGet("options")]
    [HasPermission("company-permissions:read:options")]
    public async Task<ActionResult<SelectListItemResponse<Guid>>> GetSelectListAsync(CancellationToken ct)
        => HandleResult(await _permissionService.GetSelectListAsync(ct));
}
