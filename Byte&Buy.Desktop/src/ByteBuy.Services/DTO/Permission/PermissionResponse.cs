
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Permission;

public record PermissionResponse(
    Guid Id,
    string Name,
    string Description);
