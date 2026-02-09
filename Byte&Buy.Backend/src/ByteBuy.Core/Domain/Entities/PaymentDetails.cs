using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.Domain.Entities;

public abstract class PaymentDetails : AuditableEntity, ISoftDeletable
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
