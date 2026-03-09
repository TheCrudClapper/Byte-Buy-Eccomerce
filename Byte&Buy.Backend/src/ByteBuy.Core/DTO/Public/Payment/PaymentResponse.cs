using ByteBuy.Core.Domain.Payments.Enums;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Payment;

public record PaymentResponse(
    PaymentMethod Method,
    MoneyDto PaymentTotal);