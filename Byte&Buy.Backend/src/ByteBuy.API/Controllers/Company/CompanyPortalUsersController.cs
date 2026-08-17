using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.Filtration.PortalUser;

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
    [HasPermission("{resource}:read:many")]
    public async Task<ActionResult<PagedList<PortalUserListResponse>>> GetPortalUsersListAsync(
        [FromQuery] PortalUserListQuery queryParams, CancellationToken ct)
        => HandleResult(await _portalUserService.GetPortalUsersListAsync(queryParams, ct));
}
