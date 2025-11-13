using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class RolePermission : AuditableEntity, ISoftDeletable
{
    public Guid RoleId { get; set; }
    public ApplicationRole Role { get; set; } = null!;
  
    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;

    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
