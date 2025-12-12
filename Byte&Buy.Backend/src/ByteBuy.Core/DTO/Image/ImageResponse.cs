using System;

namespace ByteBuy.Core.DTO.Image;

public record ImageResponse(
    Guid Id,
    string AltText,
    string ImagePath
    );
