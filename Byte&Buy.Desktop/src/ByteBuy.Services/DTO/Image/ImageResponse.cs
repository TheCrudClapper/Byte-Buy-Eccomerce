namespace ByteBuy.Services.DTO.Image;

public record ImageResponse(
    Guid Id,
    string ImagePath,
    string AltText
    );