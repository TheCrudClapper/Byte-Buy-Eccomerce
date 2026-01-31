using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.Domain.Entities;

public abstract class PaymentDetails : AuditableEntity
{
    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; } = null!;
    public PaymentMethod Method { get; set; }
    protected PaymentDetails(){}

    protected PaymentDetails(PaymentMethod method, Guid paymentId)
    {
        Method = method;
        PaymentId = paymentId;  
    }
}
