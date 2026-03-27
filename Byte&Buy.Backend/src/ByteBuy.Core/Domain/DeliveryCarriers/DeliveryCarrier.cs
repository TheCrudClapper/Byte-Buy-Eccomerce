using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Deliveries;
using ByteBuy.Core.Domain.DeliveryCarriers.Errors;
using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.DeliveryCarriers;

public class DeliveryCarrier : AggregateRoot, ISoftDeletable
{
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public ICollection<Delivery> Deliveries { get; private set; } = new List<Delivery>();
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private DeliveryCarrier() { }

    private DeliveryCarrier(string name, string code)
    {
        Name = name;
        Code = code.ToUpper();
        DateCreated = DateTime.UtcNow;
        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        DateDeleted = DateTime.UtcNow;
        IsActive = false;
    }

    public static Result Validate(string name, string code)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            return Result.Failure(DeliveryCarrierErrors.NameInvalid);
        if (string.IsNullOrWhiteSpace(code) || code.Length > 20)
            return Result.Failure(DeliveryCarrierErrors.CodeInvalid);

        return Result.Success();
    }

    public static Result<DeliveryCarrier> Create(string name, string code)
    {
        var validationResult = Validate(name, code);
        if (validationResult.IsFailure)
            return Result.Failure<DeliveryCarrier>(validationResult.Error);

        return new DeliveryCarrier(name, code);
    }

    public Result Update(string name, string code)
    {
        var validationResult = Validate(name, code);
        if (validationResult.IsFailure)
            return Result.Failure<DeliveryCarrier>(validationResult.Error);

        Name = name;
        Code = code;
        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }
}
