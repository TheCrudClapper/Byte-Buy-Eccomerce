using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Orders.Errors;

public static class OrderDeliveryErrors
{
    public static readonly Error InvalidCarrierCode = Error.Validation(
        "OrderDelivery.CarrierCode", "Provided Carrier Code is invalid");

    public static readonly Error InvalidParcelLocker = Error.Validation(
        "OrderDelivery.ParcelLocker", "Invalid Parcel Locker data");

    public static readonly Error InvalidPickupPointData = Error.Validation(
        "OrderDelivery.PickupPoint", "Invalid Pickup Point data");

    public static readonly Error InvalidCourierAddress = Error.Validation(
       "OrderDelivery.PickupPoint", "Invalid Courier address used in one of deliveries");

    public static readonly Error InvalidShippingAddress = Error.Validation(
        "OrderDelivery.ShippingAddress", "Shippinh address provided is not valid");
}
