using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Route("api/me")]
[ApiController]
public class UserController : BaseApiController
{
    private readonly IPortalUserService _portalUserService;
    public UserController(IPortalUserService portalUserService)
       => _portalUserService = portalUserService;

    [HttpGet]
    public async Task<ActionResult<UserBasicInfoResponse>> GetUserBasicInfoAsync()
        => HandleResult(await _portalUserService.GetBasicUserInfoAsync(CurrentUserId));

    [HttpPut]
    public async Task<ActionResult<UpdatedResponse>> PutUserBasicInfoAsync(UserBasicInfoUpdateRequest request)
        => HandleResult(await _portalUserService.PutUserBasicInfo(CurrentUserId, request));
}
