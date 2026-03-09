using ByteBuy.Core.Domain.Users.Base;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.ServiceContracts;

public interface IPasswordService
{
    /// <summary>
    /// Checks whether given password is valid with pre-configured identity 
    /// rules
    /// </summary>
    /// <param name="user">User to validate for</param>
    /// <param name="password">Password to validate</param>
    /// <returns></returns>
    Task<IdentityResult> ValidateAsync(ApplicationUser user, string password);

    /// <summary>
    /// Changes password to new one, without asking for old password
    /// </summary>
    /// <param name="user">User to change password for</param>
    /// <param name="newPassword">Password to be set for user</param>
    /// <returns></returns>
    Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword);
}