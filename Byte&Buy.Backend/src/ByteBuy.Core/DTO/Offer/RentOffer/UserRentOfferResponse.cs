using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Offer.RentOffer;

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