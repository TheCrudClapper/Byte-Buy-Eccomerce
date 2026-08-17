using ByteBuy.Core.DTO.Public.ApplicationUser;

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
    [HasPermission("{resource}:update:password")]
    public async Task<IActionResult> ChangePasswordAsync(PasswordChangeRequest request)
        => HandleResult(await _userService.ChangePasswordAsync(CurrentUserId, request));
}
