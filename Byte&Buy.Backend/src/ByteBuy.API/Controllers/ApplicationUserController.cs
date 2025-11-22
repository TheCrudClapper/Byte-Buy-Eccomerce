using ByteBuy.Core.DTO.ApplicationUser;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationUserController : BaseApiController
{
    private readonly IApplicationUserService _userService;
    public ApplicationUserController(IApplicationUserService userService)
        => _userService = userService;

    [HttpPut("password")]
    public async Task<IActionResult> ChangeUserPassword(PasswordChangeRequest request)
        => HandleResult(await _userService.ChangePassword(CurrentUserId, request));
}
