namespace ByteBuy.Services.DTO.Role;

public record RoleUpdateRequest(string Name, IList<Guid> PermissionIds);

