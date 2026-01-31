using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Http;

namespace ByteBuy.Core.Domain.Entities;

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
    public DateTime? DateDelivered { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    //Navigation EF
    public PortalUser Buyer { get; set; } = null!;
    public PaymentOrder? Payment { get; set; }
    private Order() { }
    
    private Order(Guid buyerId, OrderDelivery delivery, SellerSnapshot snapshot, IEnumerable<OrderLine> lines)
    {
        Id = Guid.NewGuid();
        BuyerId = buyerId;
        Delivery = delivery;
        Status = OrderStatus.AwaitingPayment;
        SellerSnapshot = snapshot;
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
        IEnumerable<OrderLine> lines)
    {
        return new Order(buyerId, delivery, sellerSnapshot, lines);
    }


    private void CalculateTotals()
    {
        LinesTotal = Lines
            .Select(line => line.TotalPrice)
            .Aggregate(Money.Zero, (sum, lineTotal) => sum + lineTotal);

        Total = LinesTotal + Delivery.Price;
    }

    public void PayForOrder()
    {
        Status = OrderStatus.Paid;
    }

}
