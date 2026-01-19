using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Cart.CartItem;

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
                PricePerItem = saleOffer.PricePerItem.ToMoneyDto(),
                Subtotal = (saleOffer.PricePerItem * saleCartOffer.Quantity).ToMoneyDto()
            },
            RentCartOffer rentCartOffer when rentCartOffer.Offer is RentOffer rentOffer => new RentCartOfferResponse
            {
                Id = rentCartOffer.Id,
                Image = rentOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
                Title = rentOffer.Item.Name,
                Quantity = rentCartOffer.Quantity,
                PricePerDay = rentOffer.PricePerDay.ToMoneyDto(),
                RentalDays = rentCartOffer.RentalDays,
                Subtotal = (rentOffer.PricePerDay * rentCartOffer.Quantity * rentCartOffer.RentalDays).ToMoneyDto()
            },
            _ => throw new ArgumentOutOfRangeException(nameof(cartOffer), $"Unsupported cart offer type or offer is null: {cartOffer.GetType().Name}"),
        };
    }
}
