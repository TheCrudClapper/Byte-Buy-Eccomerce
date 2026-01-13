using ByteBuy.Core.DTO.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Offer.RentOffer;

public record UserRentOfferAddRequest(
   [Required] Guid CategoryId,
   [Required] Guid ConditionId,
   [Required, MaxLength(75)] string Name,
   [Required, MaxLength(2000)] string Description,
   [Required] int QuantityAvailable,
   [Required] decimal PricePerDay,
   [Required] int MaxRentalDays,
   [Required] IEnumerable<ImageAddRequest> Images,
   [Required] IEnumerable<Guid> OtherDeliveriesIds,
   IEnumerable<Guid>? ParcelLockerDeliveries);
