using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class Condition : AuditableEntity, ISoftDeletable
{
    public string Name { get; set;  } = null!;
    public string? Description { get; set; }
    public ICollection<Item> Products { get; set; } = new List<Item>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

}
