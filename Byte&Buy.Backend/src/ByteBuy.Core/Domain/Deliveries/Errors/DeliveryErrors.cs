using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Deliveries.Errors;

/// <summary>
/// Class describes errors that might occur while working with deliveries aggregtate
/// </summary>
public static class DeliveryErrors
{

    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Delivery.NotFound", "Delivery is not found");

    public static readonly Error AlreadyExists = new(
        ErrorType.Conflict, "Delivery.AlreadyExitst", "Delivery with this data already exists.");

    public static readonly Error HasActiveRelations = new(
        ErrorType.Conflict, "Delivery.HasActiveRelations", "Delivery is used, cannot be deleted.");

    public static readonly Error NameInvalid = Error.Validation(
        "Delivery.Name", "Name is required and must be at most 50 characters.");

    public static readonly Error ParcelSizeInvalid = Error.Validation(
        "Delivery.ParcelSize", "Parcel size is allowed only for parcel lockers");

    public static readonly Error DescriptionContentInvalid = Error.Validation(
       "Delivery.Description", "Description cannot contain only whitespace.");

    public static readonly Error DescriptionLengthInvalid = Error.Validation(
       "Delivery.Description", "Description must be at most 50 characters.");

}
