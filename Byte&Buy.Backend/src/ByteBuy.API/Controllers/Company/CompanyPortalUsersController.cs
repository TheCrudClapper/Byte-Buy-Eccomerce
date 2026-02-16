using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.Filtration.PortalUser;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-portalusers")]
[Route("api/company/portal-users")]
[ApiController]
public class CompanyPortalUsersController
    : CrudControllerBase<Guid, PortalUserAddRequest, PortalUserUpdateRequest, PortalUserResponse>
{
    private readonly IPortalUserService _portalUserService;
    public CompanyPortalUsersController(IPortalUserService portalUserService) : base(portalUserService)
       => _portalUserService = portalUserService;

    [HttpGet("list")]
    [HasPermission("company-portalusers:read:many")]
    public async Task<ActionResult<PagedList<PortalUserListResponse>>> GetPortalUsersListAsync(
        [FromQuery] PortalUserListQuery queryParams, CancellationToken ct)
        => HandleResult(await _portalUserService.GetPortalUsersListAsync(queryParams, ct));
}
