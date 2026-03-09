using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Users.Entities;

namespace ByteBuy.Core.Domain.Roles.Entities;

public class Permission : Entity, ISoftDeletable
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
