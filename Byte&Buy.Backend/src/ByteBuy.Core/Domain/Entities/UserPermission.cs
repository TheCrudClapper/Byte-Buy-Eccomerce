using ByteBuy.Core.Domain.EntityContracts;
namespace ByteBuy.Core.Domain.Entities;

public class UserPermission : AuditableEntity, ISoftDeletable
{
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; } = null!;
    public Guid PermissionId { get; private set; }
    public Permission Permission { get; private set; } = null!;
    public bool IsGranted { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

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

    public void Reactivate(bool isGranted)
    {
        IsActive = true;
        DateDeleted = null;
        IsGranted = isGranted;
    }
}
