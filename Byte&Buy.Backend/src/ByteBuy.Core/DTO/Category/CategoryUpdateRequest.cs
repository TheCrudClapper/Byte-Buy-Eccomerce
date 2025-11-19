using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Category;

public record CategoryUpdateRequest(
    [Required, MaxLength(20)] string Name,
    [MaxLength(50)] string? Description
    );
