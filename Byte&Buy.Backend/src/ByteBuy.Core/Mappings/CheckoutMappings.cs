using ByteBuy.Core.DTO.Internal.Cart;
using ByteBuy.Core.DTO.Public.Cart.CartOffer;
using ByteBuy.Core.DTO.Public.Cart.Enum;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.Mappings;

public static class CheckoutMappings
{
    public static CheckoutItem MapToCheckoutItem(this FlatCartOffersQuery co)
    {
        return co.Type switch
        {
            CartOfferType.Sale => new SaleCheckoutItem
            {
                OfferId = co.OfferId,
                ItemName = co.Title,
                Thumbnail = co.Thumbnail,
                Quantity = co.Quantity,
                PricePerItem = co.PricePerItem!,
                Subtotal = new MoneyDto(co.PricePerItem!.Amount * co.Quantity, co.PricePerItem.Currency)
            },

            CartOfferType.Rent => new RentCheckoutItem
            {
                OfferId = co.OfferId,
                ItemName = co.Title,
                Thumbnail = co.Thumbnail,
                Quantity = co.Quantity,
                RentalDays = co.RentalDays!.Value,
                PricePerDay = co.PricePerDay!,
                Subtotal = new MoneyDto((co.PricePerDay!.Amount * co.RentalDays * co.Quantity).Value, co.PricePerDay.Currency)
            },

            _ => throw new InvalidOperationException("Unknown cart offer type")
        };
    }
}
