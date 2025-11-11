using ByteBuy.Core.DTO.Auth;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

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
    [Route("Login")]
    public async Task<ActionResult<TokenResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
        => HandleResult(await _authService.Login(request, cancellationToken));

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        => HandleResult(await _authService.RegisterPortalUser(request, cancellationToken));
    //{
    //    var result = await _authService.RegisterPortalUser(request, cancellationToken);
    //    if (!result.Succeeded)
    //    {
    //        var errors = result.Errors
    //       .GroupBy(e => e.Code)
    //       .ToDictionary(
    //           g => g.Key,
    //           g => g.Select(e => e.Description).ToArray()
    //       );

    //        return ValidationProblem(new ValidationProblemDetails(errors));
    //    }

    //    return NoContent();

    //}

}
