using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Country : AuditableEntity, ISoftDeletable
{
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;

    public ICollection<ShippingAddress> Addresses { get; private set; } = new List<ShippingAddress>();
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private Country() { }

    private Country(string name, string code)
    {
        Name = name;
        Code = code.ToUpper();
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

    public static Result<Country> Create(string name, string code)
    {
        var validationResult = Validate(name, code);

        return validationResult.IsFailure
            ? Result.Failure<Country>(validationResult.Error)
            : new Country(name, code);
    }

    public Result Update(string name, string code)
    {
        var validationResult = Validate(name, code);

        if (validationResult.IsFailure)
            return Result.Failure<Country>(validationResult.Error);

        Name = name;
        Code = code.ToUpper();
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

    public static Result Validate(string name, string code)
    {
        if (string.IsNullOrEmpty(name) || name.Length > 50)
            return Result.Failure(CountryErrors.NameInvalid);

        if (string.IsNullOrEmpty(code) || code.Length > 3)
            return Result.Failure(CountryErrors.CodeInvalid);

        return Result.Success();
    }
}
