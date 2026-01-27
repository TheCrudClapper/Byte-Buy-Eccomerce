using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

public record UserRentOfferResponse(
   Guid Id,
   Guid CategoryId,
   Guid ConditionId,
   string Name,
   string Description,
   int QuantityAvailable,
   MoneyDto PricePerDay,
   int MaxRentalDays,
   IReadOnlyCollection<ImageResponse> Images,
   IReadOnlyCollection<Guid>? ParcelLockerDeliveries,
   IReadOnlyCollection<Guid> OtherDeliveriesIds);