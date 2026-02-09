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

    private Payment(PaymentMethod method, PaymentStatus status)
    {
        Id = Guid.NewGuid();
        Method = method;
        Status = status;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static Result<Payment> CreateNewPayment(PaymentMethod method,
        IEnumerable<(Guid orderId, Money amount)> orders)
    {
        var payment = new Payment(method, PaymentStatus.Created);

        foreach (var (orderId, amount) in orders)
        {
            var moneyResult = Money.Create(amount.Amount, amount.Currency);
            if (moneyResult.IsFailure)
                return Result.Failure<Payment>(moneyResult.Error);

            payment.PaymentOrders.Add(PaymentOrder.Create(payment.Id, orderId, moneyResult.Value));
        }

        payment.CalculateAmount();
        return payment;
    }

    private void CalculateAmount()
    {
        Amount = PaymentOrders
            .Select(po => po.Amount)
            .Aggregate(Money.Zero, (sum, orderAmount) => sum + orderAmount);
    }

    public Result FinalizePayment(PaymentDetails details)
    {
        if (Status == PaymentStatus.Completed)
            return Result.Failure(PaymentErrors.AlreadyPaid);

        Status = PaymentStatus.Completed;
        PaymentDetails = details;

        return Result.Success();
    }
}
