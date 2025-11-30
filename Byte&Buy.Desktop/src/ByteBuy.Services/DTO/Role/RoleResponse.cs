namespace ByteBuy.Services.DTO.Role;
public record RoleResponse(Guid Id, string Name, IEnumerable<Guid> PermissionIds);