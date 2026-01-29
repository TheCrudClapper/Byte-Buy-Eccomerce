using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class PaymentOrder : AuditableEntity, ISoftDeletable
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public Money Amount { get; set; } = null!;
    public Payment Payment { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
