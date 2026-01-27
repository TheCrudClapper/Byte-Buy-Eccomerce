using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("users")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseApiController
{
    private readonly IApplicationUserService _userService;
    public UsersController(IApplicationUserService userService)
        => _userService = userService;

    [HttpPut("password")]
    public async Task<IActionResult> ChangeUserPassword(PasswordChangeRequest request)
        => HandleResult(await _userService.ChangePassword(CurrentUserId, request));
}
