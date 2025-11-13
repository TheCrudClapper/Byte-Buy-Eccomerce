using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class Permission : AuditableEntity, ISoftDeletable
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
