using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class Payment : AuditableEntity, ISoftDeletable
{
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public Money Amount { get; set; } = null!;
    public string? ExternalTransactionId { get; set; }
    public ICollection<PaymentOrder> PaymentOrders { get; set; } = [];
    public PaymentDetails PaymentDetails { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
