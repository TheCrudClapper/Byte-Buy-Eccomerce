using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Payment;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class PaymentMappings
{
    public static Expression<Func<Payment, PaymentResponse>> PaymentResponseProjection
        => p => new PaymentResponse(
            p.Method,
            p.Amount.ToMoneyDto());
}

