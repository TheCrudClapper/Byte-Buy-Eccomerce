namespace ByteBuy.Core.DTO.Category;

public record CategoryResponse(
    Guid Id,
    string Name,
    string? Description
    );
