namespace ByteBuy.Services.DTO.Role;

public record RoleResponse(Guid Id, string Name, IReadOnlyCollection<Guid> PermissionIds);