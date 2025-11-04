using ByteBuy.Core.DTO.Auth;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.ServiceContracts;

public interface IAuthService
{
    Task<Result<TokenResponse>> Login(LoginRequest request, CancellationToken ct = default);
    Task<IdentityResult> Register(RegisterRequest request, CancellationToken ct = default);
}
