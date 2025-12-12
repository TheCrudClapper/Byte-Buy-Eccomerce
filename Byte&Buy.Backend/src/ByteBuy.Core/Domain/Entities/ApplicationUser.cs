using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Identity;
namespace ByteBuy.Core.Domain.Entities;

/// <summary>
/// Represents an application user with identity, profile, and authorization information.
/// </summary>
/// <remarks>This class extends <see cref="IdentityUser{Guid}"/> to provide additional properties for user profile
/// data, permissions, roles, and soft deletion support. It is typically used to manage authentication, authorization,
/// and user-related data within the application. The <see cref="ISoftDeletable"/> interface enables tracking of deleted
/// users without permanently removing their records.</remarks>
public abstract class ApplicationUser : IdentityUser<Guid>, ISoftDeletable
{
    public string FirstName { get; protected set; } = null!;
    public string LastName { get; protected set; } = null!;
    public ICollection<UserPermission> UserPermissions { get; protected set; } = new List<UserPermission>();
    public ICollection<ApplicationUserRole> UserRoles { get; protected set; } = new List<ApplicationUserRole>();
    public ICollection<Offer> Offers { get; protected set; } = new List<Offer>();
    public bool IsActive { get; protected set; }
    public DateTime DateCreated { get; protected set; }
    public DateTime? DateEdited { get; protected set; }
    public DateTime? DateDeleted { get; protected set; }

    private ApplicationUser() { }
    protected ApplicationUser(string firstName, string lastName, string email, string? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        //Username == email within app
        UserName = email;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    protected static Result ValidateBasicInfo(string firstName, string lastName, string email, string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure(Error.Validation("First name cannot be empty!"));
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure(Error.Validation("Last name cannot be empty!"));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            return Result.Failure(Error.Validation("Email is in invalid format!"));
        if (phoneNumber is not null)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length > 15)
            {
                return Result.Failure(Error.Validation("Phone number can't be a whitespace and not longer that 15 characters"));
            }
        }
        return Result.Success();
    }

    public virtual void Deactivate()
    {
        if (!IsActive)
            return;

        var suffix = "_DELETED_" + Guid.NewGuid();
        UserName += suffix;
        NormalizedUserName += suffix.ToUpper();
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
        DeactivateAllUserPermissions();
    }

    protected Result ChangePhoneNumber(string? phoneNumber)
    {
        if (phoneNumber is not null)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length > 15)
            {
                return Result.Failure(Error.Validation("Phone number can't be a whitespace and not longer that 15 characters"));
            }
            else
            {
                PhoneNumber = phoneNumber;
            }
        }
        return Result.Success();
    }

    protected void AssignPermissionsToUser(IEnumerable<Guid> revokedPermissions,
        IEnumerable<Guid> grantedPermissions)
    {
        foreach (var id in revokedPermissions)
        {
            UserPermissions
                .Add(UserPermission.Create(Id, id, false));
        }
        foreach (var id in grantedPermissions)
        {
            UserPermissions
                .Add(UserPermission.Create(Id, id, true));
        }
    }

    protected void DeactivateAllUserPermissions()
    {
        foreach (var perm in UserPermissions)
        {
            perm.Deactivate();
        }
    }

    public Result SetPermissionOverrides(IEnumerable<Guid>? revokedPermissions,
        IEnumerable<Guid>? grantedPermissions)
    {
        //deactivate permissions that are not in given parameters
        var grantedPerms = grantedPermissions ?? [];
        var revokedPerms = revokedPermissions ?? [];

        var merge = revokedPerms
            .Concat(grantedPerms);

        foreach (var up in UserPermissions.Where(up => up.IsActive && !merge.Contains(up.PermissionId)))
        {
            up.Deactivate();
        }

        foreach (var permissionId in grantedPerms)
        {
            var existing = UserPermissions
                .FirstOrDefault(up => up.PermissionId == permissionId);

            if (existing != null)
            {
                if (!existing.IsActive || existing.IsGranted == false)
                {
                    existing.Reactivate(true);
                }
            }
            else
            {
                UserPermissions.Add(UserPermission.Create(Id, permissionId, true));
            }
        }

        foreach (var permissionId in revokedPerms)
        {
            var existing = UserPermissions
                .FirstOrDefault(up => up.PermissionId == permissionId);

            if (existing != null)
            {
                if (!existing.IsActive || existing.IsGranted)
                {
                    existing.Reactivate(false);
                }
            }
            else
            {
                UserPermissions.Add(UserPermission.Create(Id, permissionId, false));
            }
        }

        return Result.Success();
    }
}
