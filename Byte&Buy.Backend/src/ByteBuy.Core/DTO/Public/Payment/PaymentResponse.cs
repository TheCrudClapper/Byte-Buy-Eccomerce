using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Payment;

public record PaymentResponse(
    PaymentMethod Method,
    MoneyDto PaymentTotal);