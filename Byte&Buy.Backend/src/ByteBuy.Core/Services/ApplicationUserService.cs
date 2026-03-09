using ByteBuy.Core.Domain.Users.Base;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public ApplicationUserService(UserManager<ApplicationUser> userManager)
        => _userManager = userManager;

    public async Task<Result> ChangePasswordAsync(Guid userId, PasswordChangeRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure(CommonUserErrors.NotFound);

        if (!string.Equals(request.ConfirmPassword, request.NewPassword, StringComparison.Ordinal))
            return Result.Failure(AuthErrors.PasswordsDontMatch);

        var identityResult = await _userManager
            .ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!identityResult.Succeeded)
            return identityResult.ToResult();

        return Result.Success();
    }


}
