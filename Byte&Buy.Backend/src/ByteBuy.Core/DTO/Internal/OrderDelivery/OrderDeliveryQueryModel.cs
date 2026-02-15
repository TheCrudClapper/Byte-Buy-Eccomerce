using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Internal.OrderDelivery;

public sealed record OrderDeliveryQueryModel(
       string CarrierCode,
       string DeliveryName,

       // Discriminator
       DeliveryChannel Channel,

       MoneyDto Price,

       // Courier
       string? Street,
       string? HouseNumber,
       string? FlatNumber,
       string? City,
       string? PostalCity,
       string? PostalCode,

       // Pickup Point
       string? PickupPointName,
       string? PickupPointId,
       string? PickupStreet,
       string? PickupCity,
       string? PickupLocalNumber,

       // Parcel Locker
       string? ParcelLockerId
   );