using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class PaymentOrder : AuditableEntity, ISoftDeletable
{
    public Guid PaymentId { get; private set; }
    public Guid OrderId { get; private set; }
    public Money Amount { get; private set; } = null!; 
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    //EF navigation
    public Payment Payment { get; private set; } = null!;
    public Order Order { get; private set; } = null!;

    private PaymentOrder() { }

    private PaymentOrder(Guid paymentId, Guid orderId, Money amount)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Amount = amount;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static PaymentOrder Create(Guid paymentId, Guid orderId, Money amount)
        => new PaymentOrder(paymentId, orderId, amount);
   
}
