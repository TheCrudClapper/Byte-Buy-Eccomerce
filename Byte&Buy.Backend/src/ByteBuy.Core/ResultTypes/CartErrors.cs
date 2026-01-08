namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with cart aggregate
/// </summary>
public static class CartErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Cart.NotFound", "Cart is not found");

    public static readonly Error SelfOfferCartAdd
        = new (ErrorType.Conflict, "Cart.SelfOfferCartAdd", "You can't add your own offer to cart !");
}
