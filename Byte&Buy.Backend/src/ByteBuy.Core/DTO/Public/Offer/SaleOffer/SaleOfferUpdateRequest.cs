using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record SaleOfferUpdateRequest(
    [Required, Range(0, int.MaxValue)] int AdditionalQuantity,
    [Required, Range(1, double.MaxValue)] decimal PricePerItem,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);