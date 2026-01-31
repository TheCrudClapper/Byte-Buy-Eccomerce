using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Payment;

public record BlikPaymentRequest(
    [Required, MaxLength(15)] string PhoneNumber);
