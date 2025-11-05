using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public abstract class Offer : AuditableEntity, ISoftDelete
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;    
    public Guid OwnerId {  get; set; }
    public ApplicationUser Owner { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
