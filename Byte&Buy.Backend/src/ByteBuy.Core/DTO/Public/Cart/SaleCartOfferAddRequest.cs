using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Cart;

public record SaleCartOfferAddRequest(
    [Required, Range(1, int.MaxValue)] int Quantity,
    [Required] Guid OfferId);
