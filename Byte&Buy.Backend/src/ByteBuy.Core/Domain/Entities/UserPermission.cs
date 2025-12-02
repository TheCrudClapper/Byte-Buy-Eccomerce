using ByteBuy.Core.Domain.EntityContracts;
namespace ByteBuy.Core.Domain.Entities;

public class UserPermission : AuditableEntity, ISoftDeletable
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public Guid PermissionId { get; set;  }
    public Permission Permission { get; set; } = null!;
    public bool IsGranted { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    private UserPermission(Guid userId, Guid permissionId, bool isGranted)
    {
        UserId = userId;
        PermissionId = permissionId;
        IsGranted = isGranted;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static UserPermission Create(Guid userId, Guid permissionId, bool isGranted)
    {
        return new UserPermission(userId, permissionId, isGranted);
    }

    public void Deactivate()
    {
        DateDeleted = DateTime.UtcNow;
        IsActive = false;
    }
}
