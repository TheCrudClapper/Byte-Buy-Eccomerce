namespace ByteBuy.Services.DTO.Condition;

public record ConditionResponse(
    Guid Id,
    string Name,
    string? Description
    );

