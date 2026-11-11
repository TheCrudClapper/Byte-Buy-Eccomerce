using ByteBuy.Core.DTO.Public.Auth;
using Microsoft.AspNetCore.Authorization;

namespace ByteBuy.API.Controllers.Public;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController : BaseApiController
{

    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
      => _authService = authService;

    [HttpPost]
    [Route("login-employee")]
    public async Task<ActionResult<LoginResponse>> LoginEmployee(LoginRequest request)
        => HandleResult(await _authService.LoginEmployee(request));

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoginResponse>> LoginPortalUser(LoginRequest request)
        => HandleResult(await _authService.LoginPortalUser(request));

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
        => HandleResult(await _authService.RegisterPortalUser(request));

}
