using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;
public class Order : AuditableEntity, ISoftDelete
{
    public Guid UserId { get; set; }
    public PortalUser User { get; set; } = null!;
    public Money TotalAmount { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public AddressValueObj ShippingAddress { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
