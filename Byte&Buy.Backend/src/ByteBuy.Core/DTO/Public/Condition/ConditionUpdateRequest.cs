using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Condition;

public record ConditionUpdateRequest(
    [Required, MaxLength(20)] string Name,
    [MaxLength(50)] string? Description
    );