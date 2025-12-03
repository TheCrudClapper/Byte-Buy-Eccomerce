using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortalUsersController : BaseApiController
{
    private readonly IPortalUserService _portalUserService;
    public PortalUsersController(IPortalUserService portalUserService)
    {
        _portalUserService = portalUserService;
    }

    [HttpGet]
    public async Task<ActionResult> GetPortalUsersList(CancellationToken ct)
        => HandleResult(await _portalUserService.GetPortalUsersList(ct));
}
