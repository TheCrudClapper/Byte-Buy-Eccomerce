using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Money;
using ByteBuy.Core.DTO.Offer.Common;
using ByteBuy.Core.DTO.Offer.RentOffer;
using System.Linq;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class RentOfferMappings
{
    public static Expression<Func<RentOffer, RentOfferListResponse>> RentOfferListProjection
        => ro => new RentOfferListResponse(
            ro.Id,
            ro.Item.Name,
            ro.QuantityAvailable,
            ro.CreatedBy.Email!,
            ro.PricePerDay.Currency,
            ro.PricePerDay.Amount,
            ro.MaxRentalDays);


    public static Expression<Func<RentOffer, RentOfferResponse>> RentOfferResponseProjection
       => ro => new RentOfferResponse(
           ro.Id,
           ro.Item.Id,
           ro.QuantityAvailable,
           ro.PricePerDay.Amount,
           ro.MaxRentalDays,
           ro.OfferDeliveries
               .Where(d => d.Delivery.Channel == DeliveryChannelEnum.ParcelLocker)
               .Select(d => d.DeliveryId)
               .ToList(),
           ro.OfferDeliveries
               .Where(d => d.Delivery.Channel != DeliveryChannelEnum.ParcelLocker)
               .Select(d => d.DeliveryId)
               .ToList());

    public static Expression<Func<RentOffer, UserRentOfferResponse>> UserRentOfferResponseProjection
        => ro => new UserRentOfferResponse(
            ro.Id,
            ro.Item.CategoryId,
            ro.Item.ConditionId,
            ro.Item.Name,
            ro.Item.Description,
            ro.QuantityAvailable,
            new MoneyDto(ro.PricePerDay.Amount, ro.PricePerDay.Currency),
            ro.MaxRentalDays,
            ro.Item.Images
                .AsQueryable()
                .Select(ImageMappings.ImageResponseProjection)
                .ToList(),
             ro.OfferDeliveries
                .Where(d => d.Delivery.Channel == DeliveryChannelEnum.ParcelLocker)
                .Select(d => d.DeliveryId)
                .ToList(),
            ro.OfferDeliveries
                .Where(d => d.Delivery.Channel != DeliveryChannelEnum.ParcelLocker)
                .Select(d => d.DeliveryId)
                .ToList());

    public static Expression<Func<RentOffer, RentOfferDetailsResponse>> RentOfferDetailsResponseProjection
        => ro => new RentOfferDetailsResponse(
            ro.Id,
            ro.MaxRentalDays,
            ro.QuantityAvailable,
            ro.PricePerDay.ToMoneyDto(),
            ro.Item.Condition.Name,
            ro.Item.Category.Name,
            ro.Item.Description,
            ro.Item.Name,
            ro.CreatedBy is Employee
                ? new CompanySellerResponse(
                    ((Employee)ro.CreatedBy).Company.CompanyName,
                    ((Employee)ro.CreatedBy).Company.Email,
                    ((Employee)ro.CreatedBy).Company.CompanyAddress.City,
                    ((Employee)ro.CreatedBy).Company.CompanyAddress.PostalCity,
                    ((Employee)ro.CreatedBy).Company.CompanyAddress.PostalCode,
                    ((Employee)ro.CreatedBy).Company.Phone,
                    ((Employee)ro.CreatedBy).Company.TIN
                    )
                : new PrivateSellerResponse(
                    ro.CreatedBy.FirstName,
                    ro.CreatedBy.Email!,
                    ro.CreatedBy.HomeAddress!.City,
                    ro.CreatedBy.HomeAddress.PostalCity,
                    ro.CreatedBy.HomeAddress.PostalCode,
                    ro.CreatedBy.PhoneNumber
                    ),
            ro.Item.Images
                .AsQueryable()
                .Select(ImageMappings.ImageResponseProjection)
                .ToList()
            );
}
