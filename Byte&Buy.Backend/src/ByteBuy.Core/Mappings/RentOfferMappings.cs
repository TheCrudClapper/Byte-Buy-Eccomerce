using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Money;
using ByteBuy.Core.DTO.Offer.RentOffer;
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
}
