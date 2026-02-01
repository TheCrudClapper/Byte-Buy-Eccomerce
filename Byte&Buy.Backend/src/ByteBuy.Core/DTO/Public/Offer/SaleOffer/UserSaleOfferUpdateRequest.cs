using ByteBuy.Core.DTO.Public.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record UserSaleOfferUpdateRequest(
   [Required] Guid CategoryId,
   [Required] Guid ConditionId,
   [Required, MaxLength(75)] string Name,
   [Required, MaxLength(2000)] string Description,
   [Required, Range(0, int.MaxValue)] int AdditionalQuantity,
   [Required, Range(1, double.MaxValue)] decimal PricePerItem,
   [Required] IEnumerable<ExistingImageUpdateRequest> ExistingImages,
   IEnumerable<ImageAddRequest>? NewImages,
   [Required] IEnumerable<Guid> OtherDeliveriesIds,
   IEnumerable<Guid>? ParcelLockerDeliveries
);
