using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Carts.Errors;

/// <summary>
/// Class describes errors that might occur while working with cart aggregate
/// </summary>
public static class CartErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Cart.NotFound", "Cart is not found.");

    public static readonly Error OfferIsSoldOut = Error.Validation(
        "CartOffer.Offer", "Offer is sold out.");

    public static readonly Error SelfOfferCartAdd = new(
        ErrorType.Conflict, "Cart.SelfOfferCartAdd", "You can't add your own offer to cart !");

    public static readonly Error EmptyUserId = Error.Validation(
        "Cart.UserId",
        "UserId cannot be empty.");

    public static readonly Error NullOffer = Error.Validation(
        "Cart.Offer",
        "Offer cannot be null.");

    public static readonly Error QuantityInvalid = Error.Validation(
        "Cart.Quantity",
        "Quantity must be greater than 0.");

    public static readonly Error RequestedQuantityTooHigh = Error.Validation(
        "Cart.Quantity",
        "Requested quantity exceeds available quantity.");

    public static readonly Error OfferNotInCart = Error.Validation(
        "Cart.Offer",
        "Given offer does not exist in your cart.");

    public static readonly Error RentalDaysInvalid = Error.Validation(
        "Cart.RentalDays",
        "Rental period must have at least one day.");

    public static readonly Error RentalDaysTooHigh = Error.Validation(
        "Cart.RentalDays",
        "You cannot rent the item for longer than available.");
}
