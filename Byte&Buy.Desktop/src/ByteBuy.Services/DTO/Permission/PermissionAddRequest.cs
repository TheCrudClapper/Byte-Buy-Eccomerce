using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Permission;

public record PermissionAddRequest(
    string Name,
    string Description);
