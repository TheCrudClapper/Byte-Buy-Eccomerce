using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Role;

public record RoleAddRequest([Required, MaxLength(20)] string Name, [Required] IEnumerable<Guid> PermissionIds);
