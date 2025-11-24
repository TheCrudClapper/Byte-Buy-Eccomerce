using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.DTO.Auth;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Eventing.Reader;

namespace ByteBuy.Core.Domain.Entities;

/// <summary>
/// Represents an application user with identity, profile, and authorization information.
/// </summary>
/// <remarks>This class extends <see cref="IdentityUser{Guid}"/> to provide additional properties for user profile
/// data, permissions, roles, and soft deletion support. It is typically used to manage authentication, authorization,
/// and user-related data within the application. The <see cref="ISoftDeletable"/> interface enables tracking of deleted
/// users without permanently removing their records.</remarks>
public class ApplicationUser : IdentityUser<Guid>, ISoftDeletable
{
    public string FirstName { get; protected set; } = null!;
    public string LastName { get; protected set; } = null!;
    public ICollection<UserPermission> UserPermissions { get; protected set; } = new List<UserPermission>();
    public ICollection<ApplicationUserRole> UserRoles { get; protected set; } = new List<ApplicationUserRole>();
    public ICollection<Item> Items { get; protected set; } = new List<Item>();
    public bool IsActive { get; protected set; }
    public DateTime DateCreated { get; protected set; }
    public DateTime? DateEdited { get; protected set; }
    public DateTime? DateDeleted { get; protected set; } 

    private ApplicationUser() { }
    protected ApplicationUser(string firstName, string lastName, string email)
    {
        FirstName = firstName;  
        LastName = lastName;
        Email = email;

        //Username is email within app
        UserName = email;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    protected Result ChangePhoneNumber(string? phoneNumber)
    {
        if (phoneNumber is not null)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length > 15){
                return Result.Failure(Error.Validation("Phone number can't be a whitespace and not longer that 15 characters"));
            }
            else
            {
                PhoneNumber = phoneNumber;
            }
        }
        return Result.Success();
    }
}
