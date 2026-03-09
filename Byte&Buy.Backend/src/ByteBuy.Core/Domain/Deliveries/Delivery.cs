using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Deliveries.Enums;
using ByteBuy.Core.Domain.DeliveryCarriers;
using ByteBuy.Core.Domain.Offers.Entities;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Deliveries;

public class Delivery : AggregateRoot, ISoftDeletable
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public Money Price { get; private set; } = null!;
    public Guid DeliveryCarrierId { get; private set; }
    public ParcelSize? ParcelSize { get; private set; }
    public DeliveryChannel Channel { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    //EF Navigations
    public DeliveryCarrier DeliveryCarrier { get; set; } = null!;
    public ICollection<OfferDelivery> OfferDeliveries { get; set; } = new List<OfferDelivery>();

    private Delivery() { }

    private Delivery(string name,
        string? description,
        Money money,
        ParcelSize? size,
        DeliveryChannel channel,
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
        ParcelSize? size,
        DeliveryChannel channel)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            return Result.Failure(DeliveryErrors.NameInvalid);

        if (channel is not DeliveryChannel.ParcelLocker && size is not null)
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
        ParcelSize? size,
        DeliveryChannel channel,
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
        ParcelSize? size,
        DeliveryChannel channel,
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
