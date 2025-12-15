namespace ByteBuy.Core.DTO.Image;

public sealed record ImageResponse(
    Guid Id,
    string ImagePath,
    string AltText
    );
