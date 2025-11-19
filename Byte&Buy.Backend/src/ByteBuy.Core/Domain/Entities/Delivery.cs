using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;
public class Delivery : AuditableEntity, ISoftDeletable
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public Money Price { get; private set; } = null!;
    public ICollection<OfferDelivery> OfferDeliveries { get; set; } = new List<OfferDelivery>();
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private Delivery() { }

    private Delivery(string name, string? description, Money money)
    {
        Name = name;
        Description = description;
        Price = money;
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

    public static Result Validate(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            return Result.Failure(Error.Validation("Name is required and must be at most 50 characters."));

        if (description is not null)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure(Error.Validation("Description cannot contain only whitespace."));

            if (description?.Length > 50)
                return Result.Failure(Error.Validation("Description must be at most 50 characters."));
        }

        return Result.Success();
    }

    public static Result<Delivery> Create(string name, string? description, decimal price)
    {
        var validationResult = Validate(name, description);
        if (validationResult.IsFailure)
            return Result.Failure<Delivery>(validationResult.Error);

        var moneyResult = Money.Create(price, "PLN");
        if(moneyResult.IsFailure)
            return Result.Failure<Delivery>(moneyResult.Error);

        return new Delivery(name, description, moneyResult.Value);
    }

    public Result Update(string name, string? description, decimal price)
    {
        var validationResult = Validate(name, description);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        var moneyResult = Money.Create(price, "PLN");
        if (moneyResult.IsFailure)
            return Result.Failure(moneyResult.Error);

        Name = name;
        Description = description;
        Price = moneyResult.Value;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }
}
