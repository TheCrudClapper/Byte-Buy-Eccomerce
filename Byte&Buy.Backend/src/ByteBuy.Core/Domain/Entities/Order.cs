using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;
namespace ByteBuy.Core.Domain.Entities;

//Class that holds allowed transitions between order states
public static class OrderStatusTransitions
{
    public static readonly Dictionary<OrderStatus, OrderStatus[]> AllowedTransitions = new()
    {
        { OrderStatus.AwaitingPayment, [ OrderStatus.Paid, OrderStatus.Canceled ] },
        { OrderStatus.Paid, [ OrderStatus.Shipped, OrderStatus.Canceled ] },
        { OrderStatus.Shipped, [ OrderStatus.Delivered, OrderStatus.Returned ] },
        { OrderStatus.Delivered, [ OrderStatus.Returned ] },
        { OrderStatus.Canceled, Array.Empty<OrderStatus>() },
        { OrderStatus.Returned, Array.Empty<OrderStatus>() }
    };
}

//Represents one order from one seller with sellers offers
public class Order : AuditableEntity, ISoftDeletable
{
    public Guid BuyerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderDelivery Delivery { get; set; } = null!;
    public ICollection<OrderLine> Lines { get; private set; } = [];
    public Money LinesTotal { get; private set; } = null!;
    public Money Total { get; private set; } = null!;
    public SellerSnapshot SellerSnapshot { get; private set; } = null!;
    public BuyerSnapshot BuyerSnapshot { get; private set; } = null!;
    public DateTime? DateDelivered { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    //Navigation EF
    public PortalUser Buyer { get; set; } = null!;
    public PaymentOrder Payment { get; set; } = null!;
    private Order() { }

    private Order(Guid buyerId,
        OrderDelivery delivery,
        SellerSnapshot snapshot,
        BuyerSnapshot buyerSnapshot,
        IEnumerable<OrderLine> lines)
    {
        Id = Guid.NewGuid();
        BuyerId = buyerId;
        Delivery = delivery;
        Status = OrderStatus.AwaitingPayment;
        SellerSnapshot = snapshot;
        BuyerSnapshot = buyerSnapshot;
        DateCreated = DateTime.UtcNow;
        IsActive = true;

        foreach (var line in lines)
            Lines.Add(line);

        CalculateTotals();
    }

    public static Result<Order> CreateNewOrder(
        Guid buyerId,
        OrderDelivery delivery,
        SellerSnapshot sellerSnapshot,
        BuyerSnapshot buyerSnapshot,
        IEnumerable<OrderLine> lines)
    {
        return new Order(buyerId, delivery, sellerSnapshot, buyerSnapshot, lines);
    }


    private void CalculateTotals()
    {
        LinesTotal = Lines
            .Select(line => line.TotalPrice)
            .Aggregate(Money.Zero, (sum, lineTotal) => sum + lineTotal);

        Total = LinesTotal + Delivery.Price;
    }

    public Result MarkAsPaid()
      => ChangeStatus(OrderStatus.Paid);

    public Result MarkAsShipped()
        => ChangeStatus(OrderStatus.Shipped);

    public Result MarkAsDelivered()
        => ChangeStatus(OrderStatus.Delivered);

    public Result MarkAsReturned()
        => ChangeStatus(OrderStatus.Returned);

    public Result Cancel()
        => ChangeStatus(OrderStatus.Canceled);

    private bool CanChangeStatus(OrderStatus newStatus)
    {
        return OrderStatusTransitions.AllowedTransitions.TryGetValue(Status, out var allowedStatus)
               && allowedStatus.Contains(newStatus);
    }

    public Result ChangeStatus(OrderStatus newStatus)
    {
        if (!CanChangeStatus(newStatus))
        {
            return Result.Failure(Error.Validation("Order.Status", $"Cannot change status from {Status} to {newStatus}"));
        }

        Status = newStatus;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }
}

