using ByteBuy.Services.DTO.Delivery.Enum;
using ByteBuy.Services.DTO.Money;
using System.Text.Json.Serialization;

namespace ByteBuy.Services.DTO.OrderDelivery;

public sealed record CourierDeliveryDetails(
    string Street,
    string HouseNumber,
    string? FlatNumber,
    string City,
    string PostalCity,
    string PostalCode) : OrderDeliveryDetails;

public sealed record PickupPointDeliveryDetails(
    string PickupPointName,
    string PickupPointId,
    string PickupStreet,
    string PickupCity,
    string PickupLocalNumber) : OrderDeliveryDetails;

public sealed record ParcelLockerDeliveryDetails(
    string ParcelLockerId) : OrderDeliveryDetails;


[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PickupPointDeliveryDetails), "pickupPoint")]
[JsonDerivedType(typeof(ParcelLockerDeliveryDetails), "parcelLocker")]
[JsonDerivedType(typeof(CourierDeliveryDetails), "courier")]
public abstract record OrderDeliveryDetails
{
    public string CarrierCode { get; init; } = null!;
    public string DeliveryName { get; init; } = null!;
    public DeliveryChannel Channel { get; init; }
    public MoneyDto Price { get; init; } = null!;
}
