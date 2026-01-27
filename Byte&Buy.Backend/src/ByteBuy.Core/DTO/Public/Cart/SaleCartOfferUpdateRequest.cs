using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Cart;

public record SaleCartOfferUpdateRequest(
    [Required] int Quantity);
