using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Role;

public record RoleResponse(Guid Id, string Name, [Required] IReadOnlyCollection<Guid> PermissionIds);
