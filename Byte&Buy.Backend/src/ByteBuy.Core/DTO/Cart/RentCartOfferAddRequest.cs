
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Cart;

public record RentCartOfferAddRequest(
    [Required, Range(1, int.MaxValue)] int Quantity,
    [Required] Guid OfferId,
    [Required, Range(1, int.MaxValue)] int RentalDays);
