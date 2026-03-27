using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.DeliveryCarriers.Errors;

/// <summary>
/// Class describes errors that might occur while working with delivery carriers entity
/// </summary>
public static class DeliveryCarrierErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "DeliveryCarrier.NotFound", "Delivery Carrier is not found.");

    public static readonly Error AlreadyExists = new(
       ErrorType.Conflict, "DelieryCarrier.AlreadyExists", "Delivery Carrier with this data already exists.");

    public static readonly Error HasActiveDeliveries = new Error(
        ErrorType.Conflict, "DeliveryCarrier.HasActiveDeliveries", "Delivery Carrier is used, cannot be deleted.");

    public static readonly Error NameInvalid = Error.Validation(
        "DeliveryCarrier.Name", "Name is required and must be at most 50 characters.");

    public static readonly Error CodeInvalid = Error.Validation(
        "DeliveryCarrier.Code", "Code is required and must be at most 20 characters.");
}
