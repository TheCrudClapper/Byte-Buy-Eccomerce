namespace ByteBuy.Core.DTO.Public.Category;

public record CategoryResponse(
    Guid Id,
    string Name,
    string? Description);
