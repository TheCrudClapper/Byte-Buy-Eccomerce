using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.RentOffer;

public record RentOfferUpdateRequest(
    [Required] Guid ItemId,
    [Required] int QuantityAvailable,
    [Required] decimal PricePerDay,
    [Required] int MaxRentalDays,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);
