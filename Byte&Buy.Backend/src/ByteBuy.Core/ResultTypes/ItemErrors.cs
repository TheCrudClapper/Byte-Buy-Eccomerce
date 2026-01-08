namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with item aggregate
/// </summary>
public static class ItemErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Item.NotFound", "Item is not found");

    public static readonly Error HasActiveOffers = new (
        ErrorType.Conflict, "Item.HasActiveOffers", "This Item is used, cannot be deleted");
}
