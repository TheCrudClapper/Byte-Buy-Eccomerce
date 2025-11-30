using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Role;

public record RoleResponse(Guid Id, string Name, [Required] IEnumerable<Guid> PermissionIds);
