namespace ByteBuy.Core.DTO.Public.Condition;

public record ConditionResponse(
    Guid Id,
    string Name,
    string? Description
    );

