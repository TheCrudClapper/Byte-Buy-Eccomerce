namespace ByteBuy.Services.DTO.Role;

public record RoleAddRequest(string Name, IEnumerable<Guid> PermissionIds);
