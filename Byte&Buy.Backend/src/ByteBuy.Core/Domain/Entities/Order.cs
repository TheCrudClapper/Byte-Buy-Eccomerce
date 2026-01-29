using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

//Represents one order from one seller with sellers offers
public class Order : AuditableEntity, ISoftDeletable
{
    public Guid BuyerId { get; set; }
    public Guid DeliveryId { get; set; }
    public Seller Seller { get; private set; } = null!;
    public OrderStatus Status { get; set; }
    public ICollection<OrderLine> Lines { get; set; } = [];
    public Money LinesTotal { get; set; } = null!;
    public Money DeliveryPrice { get; set; } = null!;
    public Money Total { get; set; } = null!;
    public SellerSnapshot SellerSnapshot { get; set; } = null!;
    public DateTime? DateDelivered { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    //Navigation EF
    public PortalUser Buyer { get; set; } = null!;
    public OrderDelivery Delivery { get; set; } = null!;
    public PaymentOrder? Payment { get; set; }
    private Order()
    {

    }
}
