using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Auth;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Services;

public class AuthService : IAuthService
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ICartRepository _cartRepository;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ICartRepository cartRepository,
        ITokenService tokenService,
        IApplicationUserRepository userRepository)
    {
        _cartRepository = cartRepository;
        _roleManager = roleManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<Result<TokenResponse>> Login(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Failure<TokenResponse>(AuthErrors.LoginFailed);

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return Result.Failure<TokenResponse>(AuthErrors.LoginFailed);

        IList<string> roles = await _userManager.GetRolesAsync(user);

        var token = _tokenService.GenerateJwtToken(user, roles);
        return new TokenResponse(token);
    }

    public async Task<Result> RegisterPortalUser(RegisterRequest request, CancellationToken ct = default)
    {
        if (await _userRepository.ExistByEmailAsync(request.Email, ct))
            return Result.Failure(AuthErrors.AccountExists);

        var userResult = PortalUser
            .Create(request.FirstName, request.LastName, request.Email);

        if (userResult.IsFailure)
            return userResult;

        var user = userResult.Value;

        var identityResult = await _userManager
            .CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
            return identityResult.ToResult();

        var cartResult = Cart.Create(user);
        if(cartResult.IsFailure)
            return cartResult;

        user.AssignCart(cartResult.Value);
        await _cartRepository.AddCart(cartResult.Value, ct);

        const string defaultRoleName = "PortalUser";

        var roleResult = ApplicationRole.Create(defaultRoleName);

        if(roleResult.IsFailure)
            return roleResult;

        if (!await _roleManager.RoleExistsAsync(defaultRoleName))
            await _roleManager.CreateAsync(roleResult.Value);

        await _userManager.AddToRoleAsync(user, defaultRoleName);

        return Result.Success();
    }
}
