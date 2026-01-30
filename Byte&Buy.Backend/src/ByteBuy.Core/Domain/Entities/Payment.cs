using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Payment : AuditableEntity, ISoftDeletable
{
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public Money Amount { get; set; } = null!;
    public string? ExternalTransactionId { get; set; }
    public ICollection<PaymentOrder> PaymentOrders { get; set; } = [];
    public PaymentDetails? PaymentDetails { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    private Payment() { }

    private Payment(PaymentMethod method, PaymentStatus status, IEnumerable<PaymentOrder> paymentOrders)
    {
        Method = method;
        Status = status;
        
        foreach (PaymentOrder po in paymentOrders)
            PaymentOrders.Add(po);

        CalculateAmount();
    }

    public static Result<Payment> CreateNewPayment(PaymentMethod method, IEnumerable<PaymentOrder> paymentOrders)
        => new Payment(method, PaymentStatus.Created, paymentOrders);

    private void CalculateAmount()
    {
        Amount = PaymentOrders
            .Select(po => po.Amount)
            .Aggregate(Money.Zero, (sum, orderAmount) => sum + orderAmount);
    }
}
