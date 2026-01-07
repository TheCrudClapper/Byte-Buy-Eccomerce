
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.Core.ResultTypes;

public static class CartErrors
{
    public static readonly Error CartNotFound
        = new Error(404, "Cart for given user doesnt exist");

    public static readonly Error CannotAddSelfOfferToCart
        = new Error(400, "You can't add your own offer to cart !");
}
