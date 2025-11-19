using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Condition : AuditableEntity, ISoftDeletable
{
    public string Name { get; set;  } = null!;
    public string? Description { get; set; }
    public ICollection<Item> Products { get; set; } = new List<Item>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    private Condition() { }
    private Condition(string name, string? description)
    {
        Name = name;
        Description = description;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }

    public static Result<Condition> Create(string name, string? description)
    {
        var validationResult = Validate(name, description);
        
        return validationResult.IsFailure
            ? Result.Failure<Condition>(validationResult.Error)
            : new Condition(name, description);
    }

    public Result Update(string name, string? description)
    {
        var validationResult = Validate(name, description);
        if (validationResult.IsFailure)
            return Result.Failure<Condition>(validationResult.Error);

        Name = name;
        Description = description;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

    public static Result Validate(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 20)
            return Result.Failure(Error.Validation("Name is required and must be at most 20 characters."));

        if(description is not null)
        {
            if(string.IsNullOrWhiteSpace(description))
                return Result.Failure(Error.Validation("Description cannot contain only whitespace."));

            if (description?.Length > 50)
                return Result.Failure(Error.Validation("Description must be at most 50 characters."));
        }

        return Result.Success();
    }
}
