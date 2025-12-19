using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Auth;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : BaseApiController
{

    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login-employee")]
    public async Task<ActionResult<TokenResponse>> LoginEmployee(LoginRequest request)
        => HandleResult(await _authService.LoginEmployee(request));

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenResponse>> LoginPortalUser(LoginRequest request)
        => HandleResult(await _authService.LoginPortalUser(request));

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
        => HandleResult(await _authService.RegisterPortalUser(request));

}
