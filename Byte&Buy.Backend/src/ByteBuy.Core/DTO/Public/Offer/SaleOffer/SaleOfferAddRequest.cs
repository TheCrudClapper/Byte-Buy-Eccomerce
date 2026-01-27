using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record SaleOfferAddRequest(
    [Required] Guid ItemId,
    [Required] int QuantityAvailable,
    [Required] decimal PricePerItem,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);
