using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.ApplicationUser;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

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

    [HttpGet("me")]
    public async Task<ActionResult<UserBasicInfoResponse>> GetUserBasicInfo()
        => HandleResult(await _portalUserService.GetBasicUserInfoAsync(CurrentUserId));

    [HttpPut("me")]
    public async Task<ActionResult<UpdatedResponse>> PutUserBasicInfo(UserBasicInfoUpdateRequest request)
        => HandleResult(await _portalUserService.PutUserBasicInfo(CurrentUserId, request));
}
