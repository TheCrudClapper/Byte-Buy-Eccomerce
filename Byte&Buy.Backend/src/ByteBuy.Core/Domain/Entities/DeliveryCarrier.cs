using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class DeliveryCarrier : AuditableEntity, ISoftDeletable
{
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public ICollection<Delivery> Deliveries { get; private set; } = new List<Delivery>();
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }
}
