using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

//Represents one order from one seller with sellers offers
public class Order : AuditableEntity, ISoftDeletable
{
    public Guid BuyerId { get; private set; }
    public Guid DeliveryId { get; private set; }
    public OrderStatus Status { get; private set; }
    public ICollection<OrderLine> Lines { get; private set; } = [];
    public Money LinesTotal { get; private set; } = null!;
    public Money DeliveryPrice { get; private set; } = null!;
    public Money Total { get; private set; } = null!;
    public SellerSnapshot SellerSnapshot { get; private set; } = null!;
    public DateTime? DateDelivered { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    //Navigation EF
    public PortalUser Buyer { get; set; } = null!;
    public OrderDelivery Delivery { get; set; } = null!;
    public PaymentOrder? Payment { get; set; }
    private Order() { }
    
    private Order(Guid buyerId, Guid deliveryId, SellerSnapshot snapshot, Money deliveryPrice, IEnumerable<OrderLine> lines)
    {
        Id = Guid.NewGuid();
        BuyerId = buyerId;
        DeliveryId = deliveryId;
        Status = OrderStatus.AwaitingPayment;
        DeliveryPrice = deliveryPrice;
        SellerSnapshot = snapshot;
        DateCreated = DateTime.UtcNow;
        IsActive = true;

        foreach (var line in lines)
            Lines.Add(line);

        CalculateTotals();
    }

    public static Result<Order> CreateNewOrder(
        Guid buyerId,
        Guid deliveryId,
        IEnumerable<OrderLine> lines,
        decimal deliveryPriceAmount,
        string deliveryPriceCurrency,
        SellerSnapshot sellerSnapshot)
    {
        var deliveryResult = Money.Create(deliveryPriceAmount, deliveryPriceCurrency);
        if (deliveryResult.IsFailure)
            return Result.Failure<Order>(deliveryResult.Error);

        return new Order(buyerId, deliveryId, sellerSnapshot, deliveryResult.Value, lines);
    }


    private void CalculateTotals()
    {
        LinesTotal = Lines
            .Select(line => line.TotalPrice)
            .Aggregate(Money.Zero, (sum, lineTotal) => sum + lineTotal);

        Total = LinesTotal + DeliveryPrice;
    }
}
