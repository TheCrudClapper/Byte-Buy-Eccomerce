using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.PortalUser;
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

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostPortalUser(PortalUserAddRequest request)
        => HandleResult(await _portalUserService.AddPortalUser(request));

    [HttpPut("{userId}")]
    public async Task<ActionResult<UpdatedResponse>> PutPortalUser(Guid userId, PortalUserUpdateRequest request)
        => HandleResult(await _portalUserService.UpdatePortalUser(userId, request));

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeletePortalUser(Guid userId) 
        => HandleResult(await _portalUserService.DeletePortalUser(userId));

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<PortalUserListResponse>>> GetPortalUsersList(CancellationToken ct)
        => HandleResult(await _portalUserService.GetPortalUsersList(ct));

    [HttpGet("{userId}")]
    public async Task<ActionResult<PortalUserResponse>> GetPortalUser(Guid userId, CancellationToken ct)
        => HandleResult(await _portalUserService.GetPortalUser(userId, ct));
}
