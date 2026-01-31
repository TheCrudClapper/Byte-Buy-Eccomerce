using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Payment;

public record CardPaymentRequest(
    [Required] string CardHolderName,
    [Required, MinLength(16), MaxLength(19)] string CardNumber);
