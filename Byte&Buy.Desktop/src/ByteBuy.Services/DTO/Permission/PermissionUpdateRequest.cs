using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Permission;

public record PermissionUpdateRequest(
    string Name,
    string Description);
