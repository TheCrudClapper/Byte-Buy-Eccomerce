namespace ByteBuy.Core.DTO.Public.Image;

public sealed record ImageResponse(
    Guid Id,
    string ImagePath,
    string? AltText
    );
