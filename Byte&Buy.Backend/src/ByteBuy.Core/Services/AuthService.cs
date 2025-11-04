using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Auth;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public Task<Result<TokenResponse>> Login(LoginRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> Register(RegisterRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
