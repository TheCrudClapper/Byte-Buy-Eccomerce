using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Money;
using ByteBuy.Core.DTO.Offer.Common;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class SaleOfferMappings
{
    public static Expression<Func<SaleOffer, SaleOfferListResponse>> SaleOfferListProjection
        => so => new SaleOfferListResponse(
            so.Id,
            so.Item.Name,
            so.QuantityAvailable,
            so.CreatedBy.Email!,
            so.PricePerItem.Amount,
            so.PricePerItem.Currency);

    public static Expression<Func<SaleOffer, SaleOfferResponse>> SaleOfferResponseProjection
        => so => new SaleOfferResponse(
            so.Id,
            so.Item.Id,
            so.QuantityAvailable,
            so.PricePerItem.Amount,
            so.OfferDeliveries
                .Where(d => d.Delivery.Channel == DeliveryChannelEnum.ParcelLocker)
                .Select(d => d.DeliveryId)
                .ToList(),
            so.OfferDeliveries
                .Where(d => d.Delivery.Channel != DeliveryChannelEnum.ParcelLocker)
                .Select(d => d.DeliveryId)
                .ToList());

    public static Expression<Func<SaleOffer, UserSaleOfferResponse>> UserSaleOfferResponseProjection
        => so => new UserSaleOfferResponse(
            so.Id,
            so.Item.CategoryId,
            so.Item.ConditionId,
            so.Item.Name,
            so.Item.Description,
            so.QuantityAvailable,
            new MoneyDto(so.PricePerItem.Amount, so.PricePerItem.Currency),
            so.Item.Images
                .AsQueryable()
                .Select(ImageMappings.ImageResponseProjection)
                .ToList(),
            so.OfferDeliveries
                .Where(d => d.Delivery.Channel == DeliveryChannelEnum.ParcelLocker)
                .Select(d => d.DeliveryId)
                .ToList(),
            so.OfferDeliveries
                .Where(d => d.Delivery.Channel != DeliveryChannelEnum.ParcelLocker)
                .Select(d => d.DeliveryId)
                .ToList());

    public static Expression<Func<SaleOffer, SaleOfferDetailsResponse>> SaleOfferDetailsResponseProjection
        => so => new SaleOfferDetailsResponse(
            so.Id,
            so.QuantityAvailable,
            so.PricePerItem.ToMoneyDto(),
            so.Item.Condition.Name,
            so.Item.Category.Name,
            so.Item.Description,
            so.Item.Name,
             so.CreatedBy is Employee
                ? new CompanySellerResponse(
                    ((Employee)so.CreatedBy).Company.CompanyName,
                    ((Employee)so.CreatedBy).Company.Email,
                    ((Employee)so.CreatedBy).Company.CompanyAddress.City,
                    ((Employee)so.CreatedBy).Company.CompanyAddress.PostalCity,
                    ((Employee)so.CreatedBy).Company.CompanyAddress.PostalCode,
                    ((Employee)so.CreatedBy).Company.Phone,
                    ((Employee)so.CreatedBy).Company.TIN
                    )
                : new PrivateSellerResponse(
                    so.CreatedBy.FirstName,
                    so.CreatedBy.Email!,
                    so.CreatedBy.HomeAddress!.City,
                    so.CreatedBy.HomeAddress.PostalCity,
                    so.CreatedBy.HomeAddress.PostalCode,
                    so.CreatedBy.PhoneNumber
                    ),
            so.Item.Images
                .AsQueryable()
                .Select(ImageMappings.ImageResponseProjection)
                .ToList()
            );

}
