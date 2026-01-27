using ByteBuy.Core.DTO.Public.Image;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record UserSaleOfferAddRequest(
    [Required] Guid CategoryId,
    [Required] Guid ConditionId,
    [Required, MaxLength(75)] string Name,
    [Required, MaxLength(2000)] string Description,
    [Required] int QuantityAvailable,
    [Required] decimal PricePerItem,
    [Required] IEnumerable<ImageAddRequest> Images,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);
