using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class OrderItem : AuditableEntity, ISoftDeletable
{
    public Guid OrderId { get; set; }
    public Guid OfferId { get; set; }
    public Order Order { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
    public int Quantity { get; set;  }
    public Money UnitPrice { get; set; } = null!;
    public Money TotalPrice { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
