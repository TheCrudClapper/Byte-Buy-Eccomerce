using ByteBuy.Core.Domain.EntityContracts;
namespace ByteBuy.Core.Domain.Entities;

public class UserPermission : AuditableEntity, ISoftDelete
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public Guid PermissionId { get; set;  }
    public Permission Permission { get; set; } = null!;
    public bool IsGranted { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
