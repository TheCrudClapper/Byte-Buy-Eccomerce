using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Cart;

public record RentCartOfferUpdateRequest(
    [Required] int Quantity,
    [Required, Range(1, int.MaxValue)] int RentalDays);
