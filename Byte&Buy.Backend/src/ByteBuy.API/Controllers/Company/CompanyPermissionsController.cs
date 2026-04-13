using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Permission;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
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
        => _permissionService = permissionService;

    [HttpGet("options")]
    [HasPermission("company-permissions:read:options")]
    public async Task<ActionResult<SelectListItemResponse<Guid>>> GetSelectListAsync(CancellationToken ct)
        => HandleResult(await _permissionService.GetSelectListAsync(ct));

    [HttpGet]
    [HasPermission("company-permissions:read:many")]
    public async Task<ActionResult<PagedList<PermissionResponse>>> GetPermissionListAsync(
        [FromQuery] PermissionListQuery queryParams,
        CancellationToken ct)
        => HandleResult(await _permissionService.GetPermissionListAsync(queryParams, ct));
}
