using ByteBuy.Core.DTO.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Offer.SaleOffer;

public record UserSaleOfferUpdateRequest(
   [Required] Guid CategoryId,
   [Required] Guid ConditionId,
   [Required, MaxLength(75)] string Name,
   [Required, MaxLength(2000)] string Description,
   [Required] int StockQuantity,
   [Required] int QuantityAvailable,
   [Required] decimal PricePerItem,
   [Required] IEnumerable<ExistingImageUpdateRequest> ExistingImages,
   IEnumerable<ImageAddRequest>? NewImages,
   [Required] IEnumerable<Guid> OtherDeliveriesIds,
   IEnumerable<Guid>? ParcelLockerDeliveries
);
