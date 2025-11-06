using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;
public class Order : AuditableEntity, ISoftDelete
{
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
