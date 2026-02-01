using ByteBuy.Core.DTO.Public.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

public record UserRentOfferAddRequest(
   [Required] Guid CategoryId,
   [Required] Guid ConditionId,
   [Required, MaxLength(75)] string Name,
   [Required, MaxLength(2000)] string Description,
   [Required, Range(1, int.MaxValue)] int QuantityAvailable,
   [Required, Range(1, double.MaxValue)] decimal PricePerDay,
   [Required, Range(1, 360)] int MaxRentalDays,
   [Required] IEnumerable<ImageAddRequest> Images,
   [Required] IEnumerable<Guid> OtherDeliveriesIds,
   IEnumerable<Guid>? ParcelLockerDeliveries);
