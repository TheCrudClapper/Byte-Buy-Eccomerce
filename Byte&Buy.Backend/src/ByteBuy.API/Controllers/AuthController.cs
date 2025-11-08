using ByteBuy.Core.DTO.Auth;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> Login(LoginRequest request,CancellationToken cancellationToken)
            => HandleResult(await _authService.Login(request, cancellationToken));

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> Register(RegisterRequest request,CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
