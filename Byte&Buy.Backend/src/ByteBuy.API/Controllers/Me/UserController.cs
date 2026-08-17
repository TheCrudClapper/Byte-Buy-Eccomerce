using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.API.Controllers.Me;

[Resource("user")]
[Route("api/me")]
[ApiController]
public class UserController : BaseApiController
{
    private readonly IPortalUserService _portalUserService;
    public UserController(IPortalUserService portalUserService)
       => _portalUserService = portalUserService;

    [HttpGet]
    [HasPermission("{resource}:read:one")]
    public async Task<ActionResult<UserBasicInfoResponse>> GetUserBasicInfoAsync()
        => HandleResult(await _portalUserService.GetBasicInfoAsync(CurrentUserId));

    [HttpPut]
    [HasPermission("{resource}:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutUserBasicInfoAsync(UserBasicInfoUpdateRequest request)
        => HandleResult(await _portalUserService.UpdateBasicInfoAsync(CurrentUserId, request));
}
