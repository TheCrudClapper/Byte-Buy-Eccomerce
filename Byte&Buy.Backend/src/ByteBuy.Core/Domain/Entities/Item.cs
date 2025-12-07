using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class Item : AuditableEntity, ISoftDeletable
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid ConditionId { get; set; }
    public Condition Condition { get; set; } = null!;
    public int StockQuantity { get; set; }
    public Guid CreatedByUserId { get; set; }
    public ApplicationUser CreatedBy { get; set; } = null!;
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
