using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Role;

public record RoleAddRequest([Required] string Name);
