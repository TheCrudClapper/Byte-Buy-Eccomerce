using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Payments.Enums;

namespace ByteBuy.Core.Domain.Payments.Entities;

public abstract class PaymentDetails : Entity, ISoftDeletable
{
    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; } = null!;

    //Discriminator
    public PaymentMethod Method { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    protected PaymentDetails() { }

    protected PaymentDetails(PaymentMethod method, Guid paymentId)
    {
        Method = method;
        PaymentId = paymentId;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }
}
