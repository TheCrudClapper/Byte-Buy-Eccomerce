using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class Image : AuditableEntity, ISoftDelete
{
    public string ImagePath { get; set; } = null!;
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public string AltText { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
