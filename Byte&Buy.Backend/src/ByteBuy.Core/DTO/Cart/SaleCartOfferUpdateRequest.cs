using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Cart;

public record SaleCartOfferUpdateRequest(
    [Required] int Quantity);
