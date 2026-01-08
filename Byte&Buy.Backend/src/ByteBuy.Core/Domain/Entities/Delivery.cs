using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Delivery : AuditableEntity, ISoftDeletable
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public Money Price { get; private set; } = null!;
    public Guid DeliveryCarrierId { get; private set; }
    public DeliveryCarrier DeliveryCarrier { get; set; } = null!;
    public ParcelSizeEnum? ParcelSize { get; private set; }
    public DeliveryChannelEnum Channel { get; private set; }
    public ICollection<OfferDelivery> OfferDeliveries { get; set; } = new List<OfferDelivery>();
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private Delivery() { }

    private Delivery(string name,
        string? description,
        Money money,
        ParcelSizeEnum? size,
        DeliveryChannelEnum channel,
        Guid deliveryCarrierId)
    {
        Name = name;
        Description = description;
        Price = money;
        ParcelSize = size;
        Channel = channel;
        DeliveryCarrierId = deliveryCarrierId;
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

    public static Result Validate(
        string name,
        string? description,
        ParcelSizeEnum? size,
        DeliveryChannelEnum channel)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            return Result.Failure(DeliveryErrors.NameInvalid);

        if (channel is not DeliveryChannelEnum.ParcelLocker && size is not null)
            return Result.Failure(DeliveryErrors.ParcelSizeInvalid);

        if (description is not null)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure(DeliveryErrors.DescriptionContentInvalid);

            if (description?.Length > 50)
                return Result.Failure(DeliveryErrors.DescriptionLengthInvalid);
        }

        return Result.Success();
    }

    public static Result<Delivery> Create(
        string name,
        string? description,
        decimal price,
        ParcelSizeEnum? size,
        DeliveryChannelEnum channel,
        Guid deliveryCarrierId)
    {
        var validationResult = Validate(name, description, size, channel);
        if (validationResult.IsFailure)
            return Result.Failure<Delivery>(validationResult.Error);

        var moneyResult = Money.Create(price, "PLN");
        if (moneyResult.IsFailure)
            return Result.Failure<Delivery>(moneyResult.Error);

        return new Delivery(name, description, moneyResult.Value, size, channel, deliveryCarrierId);
    }

    public Result Update(string name,
        string? description,
        decimal price,
        ParcelSizeEnum? size,
        DeliveryChannelEnum channel,
        Guid deliveryCarrierId)
    {
        var validationResult = Validate(name, description, size, channel);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        var moneyResult = Money.Create(price, "PLN");
        if (moneyResult.IsFailure)
            return Result.Failure(moneyResult.Error);

        Name = name;
        Description = description;
        Price = moneyResult.Value;
        ParcelSize = size;
        Channel = channel;
        DeliveryCarrierId = deliveryCarrierId;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }
}
