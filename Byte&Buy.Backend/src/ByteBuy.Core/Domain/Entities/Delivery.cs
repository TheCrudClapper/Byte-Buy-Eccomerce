using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;
public class Delivery : AuditableEntity, ISoftDelete
{
    //comeback to
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Price { get; set; } = null!;
    public ICollection<OfferDelivery> OfferDeliveries { get; set; } = new List<OfferDelivery>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
