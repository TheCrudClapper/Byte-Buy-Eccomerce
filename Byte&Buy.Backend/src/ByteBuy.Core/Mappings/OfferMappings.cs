using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Offer.Common;
using ByteBuy.Core.DTO.Offer.Common.Query;
using ByteBuy.Core.DTO.Offer.Enum;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class OfferMappings
{
    //DEPRECATED
    //public static OfferBrowserItemResponse ToResponse(this Offer offer)
    //{
    //    return offer switch
    //    {
    //        SaleOffer saleOffer => new SaleBrowserItemResponse
    //        {
    //            Id = saleOffer.Id,
    //            Image = saleOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
    //            Title = saleOffer.Item.Name,
    //            PricePerItem = saleOffer.PricePerItem.ToMoneyDto(),
    //            Category = saleOffer.Item.Category.Name,
    //            Condition = saleOffer.Item.Condition.Name,
    //            City = offer.OwnerAddressSnapshot.City,
    //            IsCompanyOffer = saleOffer.Item.IsCompanyItem,
    //            PostalCity = offer.OwnerAddressSnapshot.PostalCity,
    //            PostalCode = offer.OwnerAddressSnapshot.PostalCode
    //        },
    //        RentOffer rentOffer => new RentBrowserItemResponse
    //        {
    //            Id = rentOffer.Id,
    //            Image = rentOffer.Item.Images.FirstOrDefault()!.ToImageResponse(),
    //            Title = rentOffer.Item.Name,
    //            PricePerDay = rentOffer.PricePerDay.ToMoneyDto(),
    //            Category = rentOffer.Item.Category.Name,
    //            Condition = rentOffer.Item.Condition.Name,
    //            MaxRentalDays = rentOffer.MaxRentalDays,
    //            City = offer.OwnerAddressSnapshot.City,
    //            IsCompanyOffer = rentOffer.Item.IsCompanyItem,
    //            PostalCity = offer.OwnerAddressSnapshot.PostalCity,
    //            PostalCode = offer.OwnerAddressSnapshot.PostalCode
    //        },
    //        _ => throw new ArgumentOutOfRangeException(nameof(offer), $"Unsupported offer type or offer is null: {offer.GetType().Name}"),
    //    };
    //}

    public static Expression<Func<Offer, UserPanelOfferQuery>> UserOfferPanelQueryProjection =>
        o => new UserPanelOfferQuery
        {
            DateCreated = o.DateCreated,
            Id = o.Id,
            DateEdited = o.DateEdited,
            QuantityAvaliable = o.QuantityAvailable,
            Title = o.Item.Name,

            Image = o.Item.Images
                .AsQueryable()
                .Select(ImageMappings.ImageResponseProjection)
                .FirstOrDefault()!,

            MaxRentalDays = o is RentOffer
                ? ((RentOffer)o).MaxRentalDays : null,

            PricePerDay = o is RentOffer
                ? ((RentOffer)o).PricePerDay.ToMoneyDto() : null,

            PricePerItem = o is SaleOffer
                ? ((SaleOffer)o).PricePerItem.ToMoneyDto()
                : null,

            Type = o is SaleOffer
                ? OfferType.Sale
                : OfferType.Rent
        };

    public static UserPanelOfferResponse ToUserOfferPanelResponse(this UserPanelOfferQuery dto)
    {
        return dto.Type switch
        {
            OfferType.Sale => new UserSalePanelResponse
            {
                Id = dto.Id,
                DateCreated = dto.DateCreated,
                DateEdited = dto.DateEdited,
                Image = dto.Image,
                Title = dto.Title,
                QuantityAvaliable = dto.QuantityAvaliable,
                PricePerItem = dto.PricePerItem!
            },
            OfferType.Rent => new UserRentPanelResponse
            {
                Id = dto.Id,
                DateCreated = dto.DateCreated,
                DateEdited = dto.DateEdited,
                Image = dto.Image,
                Title = dto.Title,
                QuantityAvaliable = dto.QuantityAvaliable,
                PricePerDay = dto.PricePerDay!,
                MaxRentalDays = dto.MaxRentalDays!.Value,
            },
            _ => throw new UnreachableException()
        };
    }

    public static Expression<Func<Offer, OfferBrowserItemQuery>> OfferBrowserItemQueryProjection
        => o => new OfferBrowserItemQuery
        {
            Condition = o.Item.Condition.Name,
            Category = o.Item.Category.Name,
            City = o.OwnerAddressSnapshot.City,
            Id = o.Id,
            Image = o.Item.Images
                .AsQueryable()
                .Select(ImageMappings.ImageResponseProjection)
                .FirstOrDefault()!,

            IsCompanyOffer = o.Item.IsCompanyItem,
            PostalCity = o.OwnerAddressSnapshot.PostalCity,
            PostalCode = o.OwnerAddressSnapshot.PostalCode,
            Title = o.Item.Name,

            PricePerItem = o is SaleOffer
                ? ((SaleOffer)o).PricePerItem.ToMoneyDto()
                : null,

            PricePerDay = o is RentOffer
                ? ((RentOffer)o).PricePerDay.ToMoneyDto()
                : null,

            MaxRentalDays = o is RentOffer
                ? ((RentOffer)o).MaxRentalDays
                : null,

            Type = o is SaleOffer
                ? OfferType.Sale
                : OfferType.Rent
        };

    public static OfferBrowserItemResponse ToBrowserItemResponse(this OfferBrowserItemQuery dto)
    {
        return dto.Type switch
        {
            OfferType.Sale => new SaleBrowserItemResponse
            {
                Id = dto.Id,
                Image = dto.Image,
                Title = dto.Title,
                Condition = dto.Condition,
                Category = dto.Category,
                City = dto.City,
                PostalCity = dto.PostalCity,
                PostalCode = dto.PostalCode,
                IsCompanyOffer = dto.IsCompanyOffer,
                PricePerItem = dto.PricePerItem!
            },

            OfferType.Rent => new RentBrowserItemResponse
            {
                Id = dto.Id,
                Image = dto.Image,
                Title = dto.Title,
                Condition = dto.Condition,
                Category = dto.Category,
                City = dto.City,
                PostalCity = dto.PostalCity,
                PostalCode = dto.PostalCode,
                IsCompanyOffer = dto.IsCompanyOffer,
                MaxRentalDays = dto.MaxRentalDays!.Value,
                PricePerDay = dto.PricePerDay!
            },

            _ => throw new UnreachableException()
        };
    }
}

