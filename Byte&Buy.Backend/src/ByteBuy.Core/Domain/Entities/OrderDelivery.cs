using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class OrderDelivery : AuditableEntity, ISoftDeletable
{
    public Guid OrderId { get; set; }

    //Delivery snapshot fields
    public string DeliveryName { get; private set; } = null!;
    public string CarrierCode { get; private set; } = null!;
    public DeliveryChannel Channel { get; private set; }
    public Money Price { get; private set; } = null!;

    // Courier 
    public string? Street { get; private set; }
    public string? HouseNumber { get; private set; }
    public string? FlatNumber { get; private set; }
    public string? City { get; private set; }
    public string? PostalCity { get; private set; }
    public string? PostalCode { get; private set; }

    // Pickup
    public string? PickupPointName { get; private set; }
    public string? PickupPointId { get; private set; }
    public string? PickupStreet { get; private set; }
    public string? PickupCity { get; private set; }
    public string? PickupLocalNumber { get; private set; }

    // Locker
    public string? ParcelLockerId { get; private set; }

    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    //Navigation Ef
    public Order Order { get; set; } = null!;

    private OrderDelivery() { }

    private OrderDelivery(Guid orderId, string deliveryName, string carrierCode, DeliveryChannel channel, Money price)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        DeliveryName = deliveryName;
        CarrierCode = carrierCode;
        Channel = channel;
        Price = price;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    private static Result<OrderDelivery> CreateInternal(
        Guid orderId,
        string deliveryName,
        string carrierCode,
        DeliveryChannel channel,
        decimal priceAmount,
        string priceCurrency)
    {
        var moneyResult = Money.Create(priceAmount, priceCurrency);
        if (moneyResult.IsFailure)
            return Result.Failure<OrderDelivery>(moneyResult.Error);

        if (string.IsNullOrWhiteSpace(carrierCode) || carrierCode.Length > 20)
            return Result.Failure<OrderDelivery>(OrderDeliveryErrors.InvalidCarrierCode);

        return new OrderDelivery(orderId, deliveryName, carrierCode, channel, moneyResult.Value);
    }

    public static Result<OrderDelivery> CreatePickupPointDelivery(
    Guid orderId,
    string deliveryName,
    string carrierCode,
    decimal priceAmount,
    string priceCurrency,
    string pickupPointName,
    string pickupPointId,
    string pickupStreet,
    string pickupCity,
    string? pickupLocalNumber)
    {
        var deliveryResult = CreateInternal(
            orderId,
            deliveryName,
            carrierCode,
            DeliveryChannel.PickupPoint,
            priceAmount,
            priceCurrency);

        if (deliveryResult.IsFailure)
            return deliveryResult;

        if (string.IsNullOrWhiteSpace(pickupPointId) ||
            string.IsNullOrWhiteSpace(pickupLocalNumber) ||
            string.IsNullOrWhiteSpace(pickupPointName) ||
            string.IsNullOrWhiteSpace(pickupStreet) ||
            string.IsNullOrWhiteSpace(pickupCity))
        {
            return Result.Failure<OrderDelivery>(
                OrderDeliveryErrors.InvalidPickupPointData);
        }

        var delivery = deliveryResult.Value;

        delivery.PickupPointId = pickupPointId;
        delivery.PickupPointName = pickupPointName;
        delivery.PickupStreet = pickupStreet;
        delivery.PickupCity = pickupCity;
        delivery.PickupLocalNumber = pickupLocalNumber;

        return delivery;
    }

    public static Result<OrderDelivery> CreateParcelLockerDelivery(
    Guid orderId,
    string deliveryName,
    string carrierCode,
    decimal priceAmount,
    string priceCurrency,
    string parcelLockerId)
    {
        var deliveryResult = CreateInternal(
            orderId,
            deliveryName,
            carrierCode,
            DeliveryChannel.ParcelLocker,
            priceAmount,
            priceCurrency);

        if (deliveryResult.IsFailure)
            return deliveryResult;

        if (string.IsNullOrWhiteSpace(parcelLockerId))
            return Result.Failure<OrderDelivery>(OrderDeliveryErrors.InvalidParcelLocker);

        var delivery = deliveryResult.Value;
        delivery.ParcelLockerId = parcelLockerId;

        return delivery;
    }

    public static Result<OrderDelivery> CreateCourierDelivery(
    Guid orderId,
    string deliveryName,
    string carrierCode,
    decimal priceAmount,
    string priceCurrency,
    string street,
    string houseNumber,
    string? flatNumber,
    string city,
    string postalCity,
    string postalCode)
    {
        var deliveryResult = CreateInternal(
            orderId,
            deliveryName,
            carrierCode,
            DeliveryChannel.Courier,
            priceAmount,
            priceCurrency);

        if (deliveryResult.IsFailure)
            return deliveryResult;

        if (string.IsNullOrWhiteSpace(street) ||
            string.IsNullOrWhiteSpace(houseNumber) ||
            string.IsNullOrWhiteSpace(city) ||
            string.IsNullOrWhiteSpace(postalCode))
        {
            return Result.Failure<OrderDelivery>(
                OrderDeliveryErrors.InvalidCourierAddress);
        }

        var delivery = deliveryResult.Value;

        delivery.Street = street;
        delivery.HouseNumber = houseNumber;
        delivery.FlatNumber = flatNumber;
        delivery.City = city;
        delivery.PostalCity = postalCity;
        delivery.PostalCode = postalCode;

        return delivery;
    }
}
