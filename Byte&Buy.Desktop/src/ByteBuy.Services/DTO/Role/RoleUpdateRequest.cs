namespace ByteBuy.Services.DTO.Role;

public record RoleUpdateRequest(string Name, IEnumerable<Guid> PermissionIds);

