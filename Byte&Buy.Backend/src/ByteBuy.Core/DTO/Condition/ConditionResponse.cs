namespace ByteBuy.Core.DTO.Condition;

public record ConditionResponse(
    Guid Id,
    string Name,
    string? Description
    );

