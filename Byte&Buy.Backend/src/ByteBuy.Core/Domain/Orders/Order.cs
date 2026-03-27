using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Orders.Entities;
using ByteBuy.Core.Domain.Orders.Enums;
using ByteBuy.Core.Domain.Orders.Errors;
using ByteBuy.Core.Domain.Shared.Exceptions;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.Domain.Users;
namespace ByteBuy.Core.Domain.Orders;

//Class that holds allowed transitions between order states
public static class OrderStatusTransitions
{
    public static readonly Dictionary<OrderStatus, OrderStatus[]> AllowedTransitions = new()
    {
        { OrderStatus.AwaitingPayment, [ OrderStatus.Paid, OrderStatus.Canceled, OrderStatus.SystemCanceled ] },
        { OrderStatus.Paid, [ OrderStatus.Shipped ] },
        { OrderStatus.Shipped, [ OrderStatus.Delivered ] },
        { OrderStatus.Delivered, [ OrderStatus.Returned ] },
        { OrderStatus.Canceled, [] },
        { OrderStatus.SystemCanceled, [] },
        { OrderStatus.Returned, [] }
    };
}

//Represents one order from one seller with sellers offers
public class Order : AggregateRoot, ISoftDeletable
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

    public Result PayForOrder()
      => ChangeStatus(OrderStatus.Paid);

    public Result ShipOrder()
        => ChangeStatus(OrderStatus.Shipped);

    public Result DeliverOrder()
    {
        var statusResult = ChangeStatus(OrderStatus.Delivered);
        if (statusResult.IsFailure)
            return Result.Failure(OrderErrors.InvalidDeliveredState);

        DateDelivered = DateTime.UtcNow;
        return Result.Success();
    }

    public Result ReturnOrder()
    {
        if (Status != OrderStatus.Delivered)
            return Result.Failure(OrderErrors.InvalidReturnState);

        if (!Lines.Any(line => line is SaleOrderLine))
            return Result.Failure(OrderErrors.NotSuitableForReturn);

        if (!DateDelivered.HasValue)
            throw new DomainInvariantException($"{nameof(DateDelivered)} " +
                $"cannot be null when order is delivered");

        var startingDate = DateDelivered.Value.Date;
        var lastReturnDay = startingDate.AddDays(14);
        if (DateTime.UtcNow.Date > lastReturnDay)
            return Result.Failure(OrderErrors.ReturnPeriodExpired);

        return ChangeStatus(OrderStatus.Returned);
    }

    public Result CancelOrder()
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
            return Result.Failure(Error.Validation("Order.OrderStatus", $"Cannot change status from {Status} to {newStatus}"));
        }

        Status = newStatus;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }
}

