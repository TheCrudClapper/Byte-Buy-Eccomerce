using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Internal.Address;
using ByteBuy.Core.DTO.Internal.Delivery;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Factories;

public static class OrderDeliveryFactory
{
    public static Result<OrderDelivery> CreateOrderDelivery(
        SellerDeliveryRequest request,
        DeliveryOrderQuery delivery,
        UserShippingAddressQuery? shippingAddress = null)
    {
        return delivery.channel switch
        {
            DeliveryChannel.Courier =>
                shippingAddress is null
                ? Result.Failure<OrderDelivery>(OrderDeliveryErrors.InvalidShippingAddress)
                : OrderDelivery.CreateCourierDelivery(
                    delivery.Name,
                    delivery.CarrierCode,
                    delivery.priceAmount,
                    delivery.priceCurrency,
                    shippingAddress.Street,
                    shippingAddress.HouseNumber,
                    shippingAddress.FlatNumber,
                    shippingAddress.City,
                    shippingAddress.PostalCity,
                    shippingAddress.PostalCode),

           DeliveryChannel.ParcelLocker =>
                OrderDelivery.CreateParcelLockerDelivery(
                    delivery.Name,
                    delivery.CarrierCode,
                    delivery.priceAmount,
                    delivery.priceCurrency,
                    request.ParcelLockerData!.LockerId),

            DeliveryChannel.PickupPoint =>
               OrderDelivery.CreatePickupPointDelivery(
                   delivery.Name,
                   delivery.CarrierCode,
                   delivery.priceAmount,
                   delivery.priceCurrency,
                   delivery.Name,
                   request.PickupPointData!.PickupPointId,
                   request.PickupPointData!.Street,
                   request.PickupPointData!.City,
                   request.PickupPointData!.LocalNumber),

            _ => throw new ArgumentOutOfRangeException(nameof(delivery), $"Unsupported delivery type or delivery is null"),
        };
    }
}
