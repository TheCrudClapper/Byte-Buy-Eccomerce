using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class PaymentOrder : AuditableEntity, ISoftDeletable
{
    public Guid PaymentId { get; set; }
    public Guid OrderGroupId { get; set; }
    public Payment Payment { get; set; } = null!;
    public OrderGroup OrderGroup { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

}
