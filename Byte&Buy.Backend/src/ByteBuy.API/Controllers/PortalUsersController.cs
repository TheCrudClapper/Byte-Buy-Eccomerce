using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.PortalUser;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("portalusers")]
[Route("api/portalusers")]
[ApiController]
public class PortalUsersController
    : CrudControllerBase<Guid, PortalUserAddRequest, PortalUserUpdateRequest, PortalUserResponse>
{
    private readonly IPortalUserService _portalUserService;
    public PortalUsersController(IPortalUserService portalUserService) : base(portalUserService)
       => _portalUserService = portalUserService;

    [HttpGet("list")]
    public async Task<ActionResult<PagedList<PortalUserListResponse>>> GetPortalUsersListAsync(
        [FromQuery] PortalUserListQuery queryParams, CancellationToken ct)
        => HandleResult(await _portalUserService.GetPortalUsersListAsync(queryParams, ct));

    [HttpGet("me")]
    public async Task<ActionResult<UserBasicInfoResponse>> GetUserBasicInfoAsync()
        => HandleResult(await _portalUserService.GetBasicUserInfoAsync(CurrentUserId));

    [HttpPut("me")]
    public async Task<ActionResult<UpdatedResponse>> PutUserBasicInfoAsync(UserBasicInfoUpdateRequest request)
        => HandleResult(await _portalUserService.PutUserBasicInfo(CurrentUserId, request));
}
