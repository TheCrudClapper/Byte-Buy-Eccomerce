using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;
public class Delivery : AuditableEntity, ISoftDelete
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Money? Price { get; set; }
    public ICollection<OfferDelivery> OfferDeliveries { get; set; } = new List<OfferDelivery>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
