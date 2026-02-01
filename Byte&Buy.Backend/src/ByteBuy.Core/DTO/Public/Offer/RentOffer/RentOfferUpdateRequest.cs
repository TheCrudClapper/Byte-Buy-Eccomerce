using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

public record RentOfferUpdateRequest(
    [Required, Range(0, int.MaxValue)] int AdditionalQuantity,
    [Required, Range(1, double.MaxValue)] decimal PricePerDay,
    [Required, Range(0, 360)] int AdditionalRentalDays,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    [Required] IEnumerable<Guid> OtherDeliveriesIds);
