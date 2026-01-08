namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with delivery carriers entity
/// </summary>
public static class DeliveryCarrierErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "DeliveryCarrier.NotFound", "Delivery Carrier is not found");

    public static readonly Error AlreadyExists = new(
       ErrorType.Conflict, "DelieryCarrier.AlreadyExists", "Delivery Carrier with this data already exists");

    public static readonly Error HasActiveDeliveries = new Error(
        ErrorType.Conflict, "DeliveryCarriers.HasActiveDeliveries", "Delivery Carrier is used, cannot be deleted");
}
