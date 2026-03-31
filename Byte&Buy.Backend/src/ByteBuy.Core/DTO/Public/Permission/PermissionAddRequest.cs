using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Permission;

public record PermissionAddRequest(
    [Required, MaxLength(100)] string Name,
    [Required, MaxLength(100)] string Description);
