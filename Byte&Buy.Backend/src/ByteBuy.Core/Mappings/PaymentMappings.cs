using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Payment;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;
public static class PaymentMappings
{
    public static Expression<Func<PaymentOrder, PaymentResponse>> PaymentResponseProjection
        => po => new PaymentResponse(po.Payment.Method, new MoneyDto(po.Payment.Amount.Amount, po.Payment.Amount.Currency));
}
