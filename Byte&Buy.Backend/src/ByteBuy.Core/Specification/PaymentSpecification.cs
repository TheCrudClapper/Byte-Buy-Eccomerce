using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Payment;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class PaymentSpecification
{
    public sealed class PaymentResponseSpec : Specification<Payment, PaymentResponse>
    {
        public PaymentResponseSpec(Guid paymentId)
        {
            Query.AsNoTracking()
                .Where(p => p.Id == paymentId)
                .Select(PaymentMappings.PaymentResponseProjection);
        }
    }
}
