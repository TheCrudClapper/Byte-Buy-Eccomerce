using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;
public class Delivery : AuditableEntity, ISoftDelete
{
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
