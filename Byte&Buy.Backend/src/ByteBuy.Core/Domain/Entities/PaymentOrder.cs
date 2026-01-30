using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class PaymentOrder : AuditableEntity, ISoftDeletable
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public Money Amount { get; set; } = null!;
    public Payment Payment { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    private PaymentOrder() { }

    private PaymentOrder(Guid paymentId, Guid orderId, Money amount)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Amount = amount;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static Result<PaymentOrder> Create(Guid paymentId, Guid orderId, decimal amountAmount, string amountCurrency)
    {
        var moneyResult = Money.Create(amountAmount, amountCurrency);
        if (moneyResult.IsFailure)
            return Result.Failure<PaymentOrder>(moneyResult.Error);

        return new PaymentOrder(paymentId, orderId, moneyResult.Value);
    }

}
