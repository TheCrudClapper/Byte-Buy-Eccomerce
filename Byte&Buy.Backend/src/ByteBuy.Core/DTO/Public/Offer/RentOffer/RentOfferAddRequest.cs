using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

public record RentOfferAddRequest(
    [Required] Guid ItemId,
    [Required, Range(1, int.MaxValue)] int QuantityAvailable,
    [Required, Range(1, double.MaxValue)] decimal PricePerDay,
    [Required, Range(1, 360)] int MaxRentalDays,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);
