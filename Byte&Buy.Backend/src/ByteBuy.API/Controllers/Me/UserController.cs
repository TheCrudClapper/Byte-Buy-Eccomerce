using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

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
    [HasPermission("user:read:one")]
    public async Task<ActionResult<UserBasicInfoResponse>> GetUserBasicInfoAsync()
        => HandleResult(await _portalUserService.GetBasicInfoAsync(CurrentUserId));

    [HttpPut]
    [HasPermission("user:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutUserBasicInfoAsync(UserBasicInfoUpdateRequest request)
        => HandleResult(await _portalUserService.UpdateBasicInfoAsync(CurrentUserId, request));
}
