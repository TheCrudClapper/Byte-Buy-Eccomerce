using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Factories;

public static class OrderLineFactory
{
    public static Result<OrderLine> FromCartOffer(
       Guid orderId,
       CartOffer cartOffer)
    {
        var image = cartOffer.Offer.Item.Images.FirstOrDefault() ?? throw new InvalidOperationException(
                $"Offer {cartOffer.Offer.Id} does not contain any images.");

        return cartOffer switch
        {
            SaleCartOffer sale =>
                SaleOrderLine.Create(
                    orderId,
                    sale.Offer.Item.Name,
                    image.ImagePath,
                    image.AltText,
                    sale.Quantity,
                    ((SaleOffer)sale.Offer).PricePerItem.Amount,
                    ((SaleOffer)sale.Offer).PricePerItem.Currency
                ).Upcast<OrderLine, SaleOrderLine>(),

            RentCartOffer rent =>
                RentOrderLine.Create(
                    orderId,
                    rent.Offer.Item.Name,
                    image.ImagePath,
                    image.AltText,
                    cartOffer.Quantity,
                    ((RentOffer)rent.Offer).PricePerDay.Amount,
                    ((RentOffer)rent.Offer).PricePerDay.Currency,
                    rent.RentalDays
                ).Upcast<OrderLine, RentOrderLine>(),

            _ => throw new ArgumentOutOfRangeException(nameof(cartOffer), $"Unsupported cartOffer type or cartOffer is null"),
        };
    }
}
