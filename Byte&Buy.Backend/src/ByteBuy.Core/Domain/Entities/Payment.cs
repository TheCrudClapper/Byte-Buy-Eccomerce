using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.Domain.Entities;

public class Payment : AuditableEntity, ISoftDeletable
{
    public PaymentStatus Status { get; set; }
    public string Currency { get; set; } = null!;
    public long AmountMinor { get; set; }
    public string StripePaymentIntentId { get; set; } = null!;
    public string StripeCheckoutSessionId { get; set; } = null!;
    public ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
