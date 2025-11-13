using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class Country : AuditableEntity, ISoftDeletable
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public ICollection<Address> Addresses = new List<Address>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
