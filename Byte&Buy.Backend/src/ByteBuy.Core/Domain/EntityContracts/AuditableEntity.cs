namespace ByteBuy.Core.Domain.EntityContracts;

/// <summary>
/// Abstract class that defines typical fields used in entities
/// </summary>
public abstract class AuditableEntity : IEntity
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
}
