using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Services;

public class PasswordService : IPasswordService
{
    private UserManager<ApplicationUser> _userManager;
    public PasswordService(UserManager<ApplicationUser> userManager)
        => _userManager = userManager;


    public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword)
    {
        //Check wheter user has password
        if (await _userManager.HasPasswordAsync(user))
        {
            var remove = await _userManager.RemovePasswordAsync(user);
            if (!remove.Succeeded)
                return remove;
        }
        //Change for new one
        var add = await _userManager.AddPasswordAsync(user, newPassword);
        return add;

    }

    public async Task<IdentityResult> ValdiateAsync(ApplicationUser user, string password)
    {
        foreach (var validator in _userManager.PasswordValidators)
        {
            var result = await validator.ValidateAsync(_userManager, user, password);
            if (!result.Succeeded)
                return result;
        }

        return IdentityResult.Success;
    }
}
