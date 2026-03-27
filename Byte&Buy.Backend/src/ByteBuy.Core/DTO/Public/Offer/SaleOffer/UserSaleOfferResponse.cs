using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Offer.Common;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record UserSaleOfferResponse(
   Guid Id,
   Guid CategoryId,
   Guid ConditionId,
   string Name,
   string Description,
   int QuantityAvailable,
   OfferAddressResponse OfferAddress,
   MoneyDto PricePerItem,
   IReadOnlyCollection<ImageResponse> Images,
   IReadOnlyCollection<Guid>? ParcelLockerDeliveries,
   IReadOnlyCollection<Guid> OtherDeliveriesIds);