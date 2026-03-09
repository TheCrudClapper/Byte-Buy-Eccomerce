using ByteBuy.Core.Domain.Base;

namespace ByteBuy.Core.Domain.Entities;

public class RolePermission : AuditableEntity, ISoftDeletable
{
    public Guid RoleId { get; private set; }
    public ApplicationRole Role { get; private set; } = null!;

    public Guid PermissionId { get; private set; }
    public Permission Permission { get; private set; } = null!;

    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    public RolePermission(Guid roleId, Guid permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static RolePermission Create(Guid roleId, Guid permissionId)
        => new RolePermission(roleId, permissionId);

    public void Deactivate()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        IsActive = true;
        DateDeleted = null;
    }
}
