using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Offer.SaleOffer;

public record UserSaleOfferResponse(
   Guid Id,
   Guid CategoryId,
   Guid ConditionId,
   string Name,
   string Description,
   int QuantityAvailable,
   MoneyDto PricePerItem,
   IReadOnlyCollection<ImageResponse> Images,
   IReadOnlyCollection<Guid>? ParcelLockerDeliveries,
   IReadOnlyCollection<Guid> OtherDeliveriesIds);