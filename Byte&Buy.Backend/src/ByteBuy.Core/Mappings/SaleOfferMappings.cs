using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.SaleOffer;
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
}
