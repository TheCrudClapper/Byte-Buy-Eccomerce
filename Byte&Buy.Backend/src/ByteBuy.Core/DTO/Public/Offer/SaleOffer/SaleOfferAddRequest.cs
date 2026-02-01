using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record SaleOfferAddRequest(
    [Required] Guid ItemId,
    [Required, Range(1, int.MaxValue)] int QuantityAvailable,
    [Required, Range(1, double.MaxValue)] decimal PricePerItem,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);
