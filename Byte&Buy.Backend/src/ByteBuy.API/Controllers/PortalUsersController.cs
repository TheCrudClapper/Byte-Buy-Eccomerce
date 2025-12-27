using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("portalusers")]
[Route("api/[controller]")]
[ApiController]
public class PortalUsersController
    : CrudControllerBase<Guid, PortalUserAddRequest, PortalUserUpdateRequest, PortalUserResponse>
{
    private readonly IPortalUserService _portalUserService;
    public PortalUsersController(IPortalUserService portalUserService) : base(portalUserService)
       => _portalUserService = portalUserService;

    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyCollection<PortalUserListResponse>>> GetPortalUsersList(CancellationToken ct)
        => HandleResult(await _portalUserService.GetPortalUsersListAsync(ct));
}
