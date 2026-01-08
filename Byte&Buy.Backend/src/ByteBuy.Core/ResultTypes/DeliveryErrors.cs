namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with deliveries aggregtate
/// </summary>
public static class DeliveryErrors
{
    public static readonly Error AlreadyExists = new(
        ErrorType.Conflict, "Delivery.AlreadyExitst", "Delivery with this data already exists");

    public static readonly Error HasActiveRelations = new(
        ErrorType.Conflict, "Delivery.HasActiveRelations", "Delivery is used, cannot be deleted");
}
