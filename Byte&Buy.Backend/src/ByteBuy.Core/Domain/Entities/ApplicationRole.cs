using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>, ISoftDelete
{
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
    public DateTime? DateDeleted { get; set; }

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
    }

    public static Result<ApplicationRole> Create(string name)
    {
        var validationResult = Validate(name);
        if (validationResult.IsFailure)
            return Result.Failure<ApplicationRole>(validationResult.Error);

        return new ApplicationRole(name);
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
}
