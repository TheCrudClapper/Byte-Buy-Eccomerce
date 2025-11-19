using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Condition;

public record ConditionAddRequest(
    [Required, MaxLength(20)] string Name,
    [MaxLength(50)] string? Description
    );

