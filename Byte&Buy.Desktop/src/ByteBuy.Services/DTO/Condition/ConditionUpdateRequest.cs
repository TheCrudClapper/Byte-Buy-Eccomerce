using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.Condition;

public record ConditionUpdateRequest(
    [Required, MaxLength(20)] string Name,
    [MaxLength(50)] string? Description
    );