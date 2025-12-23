using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.SaleOffer;

public record SaleOfferUpdateRequest(
    [Required] int QuantityAvailable,
    [Required] decimal PricePerItem,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);