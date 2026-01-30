using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Cart;
using ByteBuy.Core.DTO.Public.Cart.Enum;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Factories;

public static class OrderLineFactory
{
    public static Result<OrderLine> FromCartOffer(
       Guid orderId,
       FlatCartOffersQuery cartOffer)
    {
        return cartOffer.Type switch
        {
            CartOfferType.Sale =>
                SaleOrderLine.Create(
                    orderId,
                    cartOffer.Title,
                    cartOffer.Thumbnail.ImagePath,
                    cartOffer.Thumbnail.AltText,
                    cartOffer.Quantity,
                    cartOffer.PricePerItem!.Amount,
                    cartOffer.PricePerItem.Currency
                ).Upcast<OrderLine, SaleOrderLine>(),

            CartOfferType.Rent =>
                RentOrderLine.Create(
                    orderId,
                    cartOffer.Title,
                    cartOffer.Thumbnail.ImagePath,
                    cartOffer.Thumbnail.AltText,
                    cartOffer.Quantity,
                    cartOffer.PricePerDay!.Amount,
                    cartOffer.PricePerDay.Currency,
                    cartOffer.RentalDays!.Value
                ).Upcast<OrderLine, RentOrderLine>(),

            _ => throw new ArgumentOutOfRangeException(nameof(cartOffer), $"Unsupported cartOffer type or cartOffer is null"),
        };
    }
}
