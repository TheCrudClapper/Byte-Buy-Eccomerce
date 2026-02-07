namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with item aggregate
/// </summary>
public static class ItemErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound,
        "Item.NotFound",
        "Item is not found");

    public static readonly Error HasActiveOffers = new(
        ErrorType.Conflict,
        "Item.HasActiveOffers",
        "This Item is used, cannot be deleted");

    public static readonly Error NameInvalid = Error.Validation(
        "Item.Name",
        "Name is required and must be at most 75 characters.");

    public static readonly Error DescriptionInvalid = Error.Validation(
        "Item.Description",
        "Description is required and must be at most 2000 characters.");

    public static readonly Error StockQuantityInvalid = Error.Validation(
        "Item.StockQuantity",
        "Quantity must be at least 1.");

    public static readonly Error AdditionalStockUpdateQuantityInvalid = Error.Validation(
        "Item.StockQuantity",
        "Additional Quantity must be 0 or positive");

    public static readonly Error ImageNotFound = Error.Validation(
        "Item.Image",
        "Image with the given id was not found.");

    public static readonly Error StockNotEnough = Error.Validation(
        "Item.StockQuantity",
        "Not enough stock.");

    public static readonly Error ImageRequired = Error.Validation(
        "Item.Image",
        "At least one image is required for item !");

    public static readonly Error StockNotSupported = Error.Validation(
        "Item.StockQuantity",
        "You are not a company, thus stock mainpulation is not supported");
}
