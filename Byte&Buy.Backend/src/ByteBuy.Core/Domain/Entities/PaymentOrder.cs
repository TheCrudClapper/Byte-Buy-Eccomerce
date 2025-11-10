using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class PaymentOrder : AuditableEntity, ISoftDelete
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public Payment Payment { get; set; } = null!;
    public Order Order { get; set; } = null!;
    //audit snapshot
    public long AmountMinor { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
