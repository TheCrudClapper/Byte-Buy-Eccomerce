using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Category;

public record CategoryAddRequest(
    [Required, MaxLength(20)] string Name,
    [MaxLength(50)] string? Description
    );
