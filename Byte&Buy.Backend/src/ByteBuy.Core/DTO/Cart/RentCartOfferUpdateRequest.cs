using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Cart;

public record RentCartOfferUpdateRequest(
    [Required] int Quantity,
    [Required, Range(1, int.MaxValue)] int RentalDays);
