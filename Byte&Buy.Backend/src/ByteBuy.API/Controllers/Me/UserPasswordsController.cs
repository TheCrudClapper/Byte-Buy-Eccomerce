using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Resource("users")]
[Route("api/me")]
[ApiController]
public class UserPasswordsController : BaseApiController
{
    private readonly IApplicationUserService _userService;
    public UserPasswordsController(IApplicationUserService userService)
        => _userService = userService;

    [HttpPut("password")]
    [HasPermission("users:update:password")]
    public async Task<IActionResult> ChangeUserPassword(PasswordChangeRequest request)
        => HandleResult(await _userService.ChangePassword(CurrentUserId, request));
}
