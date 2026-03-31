using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Permissions.Errors;
using ByteBuy.Core.Domain.Roles.Entities;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Users.Entities;

namespace ByteBuy.Core.Domain.Permissions;

public class Permission : AggregateRoot, ISoftDeletable
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    //EF Core Navigation Properties
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
    public ICollection<UserPermission> UserPermissions { get; set; } = [];

    private Permission() { }

    private Permission(string name, string description)
    {
        Name = name;
        Description = description;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static Result<Permission> Create(string name, string description)
    {
        var validation = Validate(name, description);
        if (validation.IsFailure)
            return Result.Failure<Permission>(validation.Error);

        return new Permission(name, description);
    }

    public Result Update(string name, string description)
    {
        var validation = Validate(name, description);
        if(validation.IsFailure)
            return Result.Failure(validation.Error);

        Name = name;
        Description = description;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

    public static Result Validate(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            return Result.Failure(PermissionErrors.InvalidName);

        if (string.IsNullOrWhiteSpace(description) || description.Length > 100)
            return Result.Failure(PermissionErrors.InvalidDescription);

        return Result.Success();
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }
}
