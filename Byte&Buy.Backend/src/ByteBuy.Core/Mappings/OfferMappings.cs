using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Offer.Common;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Offer.SaleOffer;

namespace ByteBuy.Core.Mappings;

public static class OfferMappings
{
    public static OfferBrowserItemResponse ToResponse(this Offer offer)
    {
        return offer switch
        {
            SaleOffer saleOffer => new SaleBrowserItemResponse
            {
                Id = saleOffer.Id,
                Image = saleOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
                Title = saleOffer.Item.Name,
                PricePerItem = saleOffer.PricePerItem.ToMoneyDto(),
                Category = saleOffer.Item.Category.Name,
                Condition = saleOffer.Item.Condition.Name,
                City = offer.OwnerAddressSnapshot.City,
                IsCompanyOffer = saleOffer.Item.IsCompanyItem,
                PostalCity = offer.OwnerAddressSnapshot.PostalCity,
                PostalCode = offer.OwnerAddressSnapshot.PostalCode
            },
            RentOffer rentOffer => new RentBrowserItemResponse
            {
                Id = rentOffer.Id,
                Image = rentOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
                Title = rentOffer.Item.Name,
                PricePerDay = rentOffer.PricePerDay.ToMoneyDto(),
                Category = rentOffer.Item.Category.Name,
                Condition = rentOffer.Item.Condition.Name,
                MaxRentalDays = rentOffer.MaxRentalDays,
                City = offer.OwnerAddressSnapshot.City,
                IsCompanyOffer = rentOffer.Item.IsCompanyItem,
                PostalCity = offer.OwnerAddressSnapshot.PostalCity,
                PostalCode = offer.OwnerAddressSnapshot.PostalCode
            },
            _ => throw new ArgumentOutOfRangeException(nameof(offer), $"Unsupported offer type or offer is null: {offer.GetType().Name}"),
        };
    }

    public static UserPanelOfferResponse ToUserOfferPanelResponse(this Offer offer)
    {
        return offer switch
        {
            SaleOffer saleOffer => new UserSalePanelResponse
            {
                Id = saleOffer.Id,
                DateCreated = saleOffer.DateCreated,
                DateEdited = saleOffer.DateEdited,
                Image = saleOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
                Title = saleOffer.Item.Name,
                QuantityAvaliable = saleOffer.QuantityAvailable,
                PricePerItem = saleOffer.PricePerItem.ToMoneyDto()
            },
            RentOffer rentOffer => new UserRentPanelResponse
            {

                Id = rentOffer.Id,
                DateCreated = rentOffer.DateCreated,
                DateEdited = rentOffer.DateEdited,
                Image = rentOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
                Title = rentOffer.Item.Name,
                QuantityAvaliable = rentOffer.QuantityAvailable,
                PricePerDay = rentOffer.PricePerDay.ToMoneyDto(),
                MaxRentalDays = rentOffer.MaxRentalDays,
            },
            _ => throw new ArgumentOutOfRangeException(nameof(offer), $"Unsupported offer type or offer is null: {offer.GetType().Name}"),
        };
    }
}

