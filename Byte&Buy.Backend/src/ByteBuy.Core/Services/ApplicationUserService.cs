using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.ApplicationUser;
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

    public async Task<Result> ChangePassword(Guid userId, PasswordChangeRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure(Error.NotFound);

        if (string.Equals(request.ConfirmPassword, request.NewPassword, StringComparison.Ordinal))
            return Result.Failure(ApplicationUserErrors.PasswordsDontMatch);

        var identityResult = await _userManager
            .ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!identityResult.Succeeded)
            return identityResult.ToResult();

        return Result.Success();
    }
}
