using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.Cart.Enum;
using ByteBuy.Core.DTO.Internal.Checkout;
using ByteBuy.Core.DTO.Public.Cart.CartOffer;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.DTO.Public.Money;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class CheckoutMappings
{
    public static CheckoutItem MapToCheckoutItem(this CheckoutItemQuery co)
    {
        return co.Type switch
        {
            CartOfferType.Sale => new SaleCheckoutItem
            {
                OfferId = co.OfferId,
                ItemName = co.Title,
                AvaliableQuantity = co.AvaliableQuantity,
                CanFinalize = co.CanFinalize,
                Status = co.Status,
                Thumbnail = co.Thumbnail,
                Quantity = co.Quantity,
                PricePerItem = co.PricePerItem!,
                Subtotal = new MoneyDto(co.PricePerItem!.Amount * co.Quantity, co.PricePerItem.Currency)
            },

            CartOfferType.Rent => new RentCheckoutItem
            {
                OfferId = co.OfferId,
                ItemName = co.Title,
                AvaliableQuantity = co.AvaliableQuantity,
                CanFinalize = co.CanFinalize,
                Status = co.Status,
                Thumbnail = co.Thumbnail,
                Quantity = co.Quantity,
                RentalDays = co.RentalDays!.Value,
                PricePerDay = co.PricePerDay!,
                Subtotal = new MoneyDto((co.PricePerDay!.Amount * co.RentalDays * co.Quantity).Value, co.PricePerDay.Currency)
            },

            _ => throw new InvalidOperationException("Unknown cart offer type")
        };
    }

    public static Expression<Func<CartOffer, CheckoutItemQuery>> CheckoutItemQueryProjection
        => co => new CheckoutItemQuery
        {
            OfferId = co.OfferId,
            Quantity = co.Quantity,
            AvaliableQuantity = co.Offer.QuantityAvailable,
            Status = co.Offer.Status,
            PricePerItem = co.Offer is SaleOffer
                ? ((SaleOffer)co.Offer).PricePerItem.ToMoneyDto()
                : null,

            PricePerDay = co.Offer is RentOffer
                ? ((RentOffer)co.Offer).PricePerDay.ToMoneyDto()
                : null,

            RentalDays = co is RentCartOffer
                ? ((RentCartOffer)co).RentalDays
                : null,

            SellerId = co.Offer.Seller.Id,
            SellerType = co.Offer.Seller.Type,
            Thumbnail = co.Offer.Item.Images
                .AsQueryable()
                .Select(ImageMappings.ImageThumbnailProjection)
                .FirstOrDefault()!,

            Title = co.Offer.Item.Name,

            Type = co is RentCartOffer
                ? CartOfferType.Rent
                : CartOfferType.Sale,

            AvaliableDeliveriesIds = co.Offer.OfferDeliveries
                .Select(d => d.DeliveryId)
                .ToList(),

            CanFinalize =
                co.Offer.Status == OfferStatus.Available
                && co.Quantity <= co.Offer.QuantityAvailable
        };
}
