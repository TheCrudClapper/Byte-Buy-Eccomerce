using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Roles.Entities;
using ByteBuy.Core.Domain.Roles.Errors;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Domain.Roles;

//Aggregate Root
public class ApplicationRole : IdentityRole<Guid>, ISoftDeletable, IEntity
{
    public ICollection<RolePermission> RolePermissions { get; private set; } = [];
    public ICollection<ApplicationUserRole> UserRoles { get; private set; } = [];
    public bool IsSystemRole { get; private set; } = false;
    public bool IsActive { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime? DateEdited { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private ApplicationRole() { }
    private ApplicationRole(string name)
    {
        Name = name;
        IsActive = true;
        IsSystemRole = false;
        DateCreated = DateTime.UtcNow;
    }

    private static Result Validate(string Name)
    {
        if (string.IsNullOrWhiteSpace(Name) || Name.Length > 20)
            return Result.Failure(RoleErrors.InvalidName);

        return Result.Success();
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;

        //Marking deactivated role with suffix to avoid hitting NormalizedName constraint
        var suffix = "_DELETED_" + Guid.NewGuid();
        Name += suffix;
        NormalizedName += suffix.ToUpper();

        DeactiveAllRolePermissions();
    }

    public static Result<ApplicationRole> Create(string name, IEnumerable<Guid> permissionIds)
    {
        var validationResult = Validate(name);
        if (validationResult.IsFailure)
            return Result.Failure<ApplicationRole>(validationResult.Error);

        var role = new ApplicationRole(name);

        if (permissionIds is null || !permissionIds.Any())
            return Result.Failure<ApplicationRole>(RoleErrors.PermissionIsRequired);

        role.AssignPermissionsToRole(permissionIds);

        return role;
    }

    /// <summary>
    /// Method that assigns permission to newly created role.
    /// Used ONLY in role create actions.
    /// </summary>
    /// <param name="PermissionIds"></param>
    private void AssignPermissionsToRole(IEnumerable<Guid> PermissionIds)
    {
        foreach (var id in PermissionIds)
        {
            RolePermissions.Add(RolePermission.Create(Id, id));
        }
    }

    public Result Update(string name, IEnumerable<Guid> permissionIds)
    {
        var validationResult = Validate(name);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        Name = name;
        DateEdited = DateTime.UtcNow;

        var permissionSetResult = SetPermissions(permissionIds);
        if (permissionSetResult.IsFailure)
            return permissionSetResult;

        return Result.Success();
    }

    private void DeactiveAllRolePermissions()
    {
        foreach (var rolePerm in RolePermissions)
            rolePerm.Deactivate();
    }

    public Result SetPermissions(IEnumerable<Guid> PermissionIds)
    {
        if (PermissionIds is null || !PermissionIds.Any())
            return Result.Failure(RoleErrors.PermissionIsRequired);

        var newPermissionIds = PermissionIds.Distinct().ToList();

        //deactivate permissions not present in request
        foreach (var rp in RolePermissions.Where(rp => rp.IsActive && !newPermissionIds.Contains(rp.PermissionId)))
        {
            rp.Deactivate();
        }

        foreach (var permissionId in newPermissionIds)
        {
            var existing = RolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId);
            if (existing is not null)
            {
                if (!existing.IsActive)
                {
                    existing.Reactivate();
                }
            }
            else
            {
                RolePermissions.Add(RolePermission.Create(Id, permissionId));
            }
        }

        return Result.Success();
    }
}
