using ByteBuy.Core.DTO.Public.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

public record UserRentOfferUpdateRequest(
   [Required] Guid CategoryId,
   [Required] Guid ConditionId,
   [Required, MaxLength(75)] string Name,
   [Required, MaxLength(2000)] string Description,
   [Required, Range(0, int.MaxValue)] int AdditionalQuantity,
   [Required, Range(1, double.MaxValue)] decimal PricePerDay,
   [Required, Range(0, 360)] int AdditionalRentalDays,
   [Required] IEnumerable<ExistingImageUpdateRequest> ExistingImages,
   IEnumerable<ImageAddRequest>? NewImages,
   [Required] IEnumerable<Guid> OtherDeliveriesIds,
   IEnumerable<Guid>? ParcelLockerDeliveries);

