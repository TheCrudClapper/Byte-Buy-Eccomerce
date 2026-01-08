using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Category : AuditableEntity, ISoftDeletable
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public ICollection<Item> Products { get; set; } = new List<Item>();
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private Category() { }

    private Category(string name, string? description)
    {
        Name = name;
        Description = description;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static Result<Category> Create(string name, string? description)
    {
        var validationResult = Validate(name, description);
        return validationResult.IsFailure
            ? Result.Failure<Category>(validationResult.Error)
            : new Category(name, description);
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }

    public Result Update(string name, string? description)
    {
        var validationResult = Validate(name, description);
        if (validationResult.IsFailure)
            return Result.Failure<Category>(validationResult.Error);

        Name = name;
        Description = description;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

    private static Result Validate(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 20)
            return Result.Failure(CategoryErrors.NameInvalid);

        if (description is not null)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure(CategoryErrors.DescriptionContentInvalid);

            if (description?.Length > 50)
                return Result.Failure(CategoryErrors.DescriptionLengthInvalid);
        }

        return Result.Success();
    }
}
