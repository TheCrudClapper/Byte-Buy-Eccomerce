using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class OrderItem : AuditableEntity, ISoftDelete
{

    public Guid OrderId { get; set; }
    public Guid OfferId { get; set; }
    public Order Order { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
