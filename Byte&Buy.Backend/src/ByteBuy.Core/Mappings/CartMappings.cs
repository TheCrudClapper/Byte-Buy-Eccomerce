using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Cart;
using ByteBuy.Core.DTO.Internal.Cart.Enum;
using ByteBuy.Core.DTO.Public.Cart.CartOffer;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class CartMappings
{
    public static CartOfferResponse ToCartItemResponse(this CartOffer cartOffer)
    {
        return cartOffer switch
        {
            SaleCartOffer saleCartOffer when saleCartOffer.Offer is SaleOffer saleOffer => new SaleCartOfferResponse
            {
                Id = saleCartOffer.Id,
                Image = saleOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
                Title = saleOffer.Item.Name,
                Quantity = saleCartOffer.Quantity,
                OfferId = saleCartOffer.OfferId,
                PricePerItem = saleOffer.PricePerItem.ToMoneyDto(),
                Subtotal = (saleOffer.PricePerItem * saleCartOffer.Quantity).ToMoneyDto()
            },
            RentCartOffer rentCartOffer when rentCartOffer.Offer is RentOffer rentOffer => new RentCartOfferResponse
            {
                Id = rentCartOffer.Id,
                Image = rentOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
                Title = rentOffer.Item.Name,
                Quantity = rentCartOffer.Quantity,
                OfferId = rentCartOffer.OfferId,
                PricePerDay = rentOffer.PricePerDay.ToMoneyDto(),
                RentalDays = rentCartOffer.RentalDays,
                Subtotal = (rentOffer.PricePerDay * rentCartOffer.Quantity * rentCartOffer.RentalDays).ToMoneyDto()
            },
            _ => throw new ArgumentOutOfRangeException(nameof(cartOffer), $"Unsupported cart offer type or offer is null: {cartOffer.GetType().Name}"),
        };
    }

    public static Expression<Func<CartOffer, FlatCartOffersQuery>> FlatCartOffersProjection
        => co => new FlatCartOffersQuery
        {
            OfferId = co.OfferId,
            Quantity = co.Quantity,

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
                .ToList()
        };
}
