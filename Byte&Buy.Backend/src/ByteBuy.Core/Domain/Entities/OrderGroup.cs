using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

//Represents a collection of order from many sellers at once
public class OrderGroup : AuditableEntity, ISoftDeletable
{
    public Guid BuyerId { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
    public Money TotalPrice { get; set; } = null!;
    public OrderGroupStatus Status { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    //Navigation EF
    public PortalUser Buyer { get; set; } = null!;
    public ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();
}
