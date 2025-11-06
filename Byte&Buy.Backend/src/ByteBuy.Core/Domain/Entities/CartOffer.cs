using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class CartOffer : AuditableEntity, ISoftDelete
{
    public int Quantity { get; set; }
    public Guid CartId { get; set; }
    public Guid OfferId { get; set; }
    public Cart Cart { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
