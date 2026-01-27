using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Category;

public record CategoryAddRequest(
    [Required, MaxLength(20)] string Name,
    [MaxLength(50)] string? Description
    );
