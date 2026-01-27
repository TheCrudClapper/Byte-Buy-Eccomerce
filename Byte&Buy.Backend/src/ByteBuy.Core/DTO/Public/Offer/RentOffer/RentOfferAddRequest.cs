using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

public record RentOfferAddRequest(
    [Required] Guid ItemId,
    [Required] int QuantityAvailable,
    [Required] decimal PricePerDay,
    [Required] int MaxRentalDays,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);
