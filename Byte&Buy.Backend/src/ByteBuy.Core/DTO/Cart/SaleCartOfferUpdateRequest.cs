using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Cart;

public record SaleCartOfferUpdateRequest(
    [Required, Range(1, int.MaxValue)] int Quantity);
