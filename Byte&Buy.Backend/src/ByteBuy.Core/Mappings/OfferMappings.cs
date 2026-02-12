using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.Offer;
using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.DTO.Public.Offer.Enum;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Pagination;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class OfferMappings
{

    public static Expression<Func<Offer, UserPanelOfferQuery>> UserOfferPanelQueryProjection =>
        o => new UserPanelOfferQuery
        {
            DateCreated = o.DateCreated,
            Id = o.Id,
            DateEdited = o.DateEdited,
            QuantityAvaliable = o.QuantityAvailable,
            Title = o.Item.Name,
            Status = o.Status,
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
                Status = dto.Status,
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
                Status = dto.Status,
                Image = dto.Image,
                Title = dto.Title,
                QuantityAvaliable = dto.QuantityAvaliable,
                PricePerDay = dto.PricePerDay!,
                MaxRentalDays = dto.MaxRentalDays!.Value,
            },
            _ => throw new NotSupportedException($"OrderStatus {dto.Status} is not a valid offer status")
        };
    }

    public static Expression<Func<Offer, OfferBrowserItemQuery>> OfferBrowserItemQueryProjection
        => o => new OfferBrowserItemQuery
        {
            Condition = o.Item.Condition.Name,
            Category = o.Item.Category.Name,
            City = o.OfferAddressSnapshot.City,
            Id = o.Id,
            Image = o.Item.Images
                .AsQueryable()
                .Select(ImageMappings.ImageResponseProjection)
                .FirstOrDefault()!,

            Status = o.Status,
            IsCompanyOffer = o.Seller.Type == SellerType.Company,
            PostalCity = o.OfferAddressSnapshot.PostalCity,
            PostalCode = o.OfferAddressSnapshot.PostalCode,
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
                Status = dto.Status,
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
                Status = dto.Status,
                Condition = dto.Condition,
                Category = dto.Category,
                City = dto.City,
                PostalCity = dto.PostalCity,
                PostalCode = dto.PostalCode,
                IsCompanyOffer = dto.IsCompanyOffer,
                MaxRentalDays = dto.MaxRentalDays!.Value,
                PricePerDay = dto.PricePerDay!
            },

            _ => throw new NotSupportedException($"OrderStatus {dto.Status} is not a valid offer status")
        };
    }

    public static PagedList<OfferBrowserItemResponse> ToPagedList(this PagedList<OfferBrowserItemQuery> pagetResult)
    {
        return new PagedList<OfferBrowserItemResponse>()
        {
            Items = pagetResult.Items.Select(o => o.ToBrowserItemResponse()).ToList(),
            Metadata = pagetResult.Metadata
        };
    }
}

