using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Country : AuditableEntity, ISoftDeletable
{
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;

    public ICollection<Address> Addresses = new List<Address>();
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private Country(){}

    private Country(string name, string code)
    {
        Name = name;
        Code = code.ToUpper();
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }

    public static Result<Country> Create(string name, string code)
    {
        var validationResult = Country.Validate(name, code);

        return validationResult.IsFailure
            ? Result.Failure<Country>(validationResult.Error)
            : new Country(name, code);
    }

    public Result Update(string name, string code)
    {
        var validationResult = Validate(name, code);

        if(validationResult.IsFailure)
            return Result.Failure<Country>(validationResult.Error);

        Name = name;
        Code = code.ToUpper();
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

    public static Result Validate(string name, string code)
    {
        if (string.IsNullOrEmpty(name) || name.Length > 50)
            return Result.Failure(Error.Validation("Name is required and must be at most 50 characters."));

        if (string.IsNullOrEmpty(code) || code.Length > 3)
            return Result.Failure(Error.Validation("Code is required and must be at most 3 characters."));

        return Result.Success();
    }
}
