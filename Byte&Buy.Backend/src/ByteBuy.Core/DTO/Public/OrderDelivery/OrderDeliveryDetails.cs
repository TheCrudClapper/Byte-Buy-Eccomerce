using ByteBuy.Core.Domain.Deliveries.Enums;
using ByteBuy.Core.DTO.Public.Money;
using System.Text.Json.Serialization;

namespace ByteBuy.Core.DTO.Public.OrderDelivery;


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

