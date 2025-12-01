using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>, ISoftDeletable
{
    public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
    public ICollection<ApplicationUserRole> UserRoles { get; private set; } = new List<ApplicationUserRole>();
    public bool IsActive { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime? DateEdited { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private ApplicationRole() { }
    private ApplicationRole(string name)
    {
        Name = name;
        NormalizedName = name.ToUpper();
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    private static Result Validate(string Name)
    {
        if (string.IsNullOrWhiteSpace(Name) || Name.Length > 20)
            return Result.Failure(Error.Validation("Name is required and must be at most 20 characters."));

        return Result.Success();
    }

    public void Deactivate()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
        DeactiveAllRolePermissions();
    }

    public static Result<ApplicationRole> Create(string name, IEnumerable<Guid> permissionIds)
    {
        var validationResult = Validate(name);
        if (validationResult.IsFailure)
            return Result.Failure<ApplicationRole>(validationResult.Error);

        var role =  new ApplicationRole(name);

        if(!permissionIds.Any() || permissionIds is null)
            return Result.Failure<ApplicationRole>(Error.Validation("Role need to have at least one Permission"));

        role.AssignPermissionsToRole(permissionIds);

        return role;
    }

    //Method used only in adding new role
    private void AssignPermissionsToRole(IEnumerable<Guid> PermissionIds)
    {
        foreach(var id in PermissionIds)
        {
            RolePermissions.Add(RolePermission.Create(Id, id));
        }
    }

    public Result Update(string name)
    {
        var validationResult = Validate(name);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        Name = name;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

    private void DeactiveAllRolePermissions()
    {
        foreach(var rolePerm in RolePermissions)
        {
            rolePerm.Deactivate();
        }
    }

    public Result SetPermissions(IEnumerable<Guid> PermissionIds)
    {
        if (!PermissionIds.Any() || PermissionIds is null)
            return Result.Failure(Error.Validation("Role need to have at least one Permission"));

        var newPermissionIds = PermissionIds.Distinct().ToList();

        //deactivate permissions not present in request
        foreach(var rp in RolePermissions.Where(rp => rp.IsActive && !newPermissionIds.Contains(rp.PermissionId)))
        {
            rp.Deactivate();
        }

        foreach(var permissionId in newPermissionIds)
        {
            var existing = RolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId);
            if(existing != null)
            {
                if (!existing.IsActive)
                {
                    existing.IsActive = true;
                    existing.DateDeleted = null;
                }
            }
            else
            {
                RolePermissions.Add(RolePermission.Create(this.Id, permissionId));
            }
        }

        return Result.Success();
    }
}
