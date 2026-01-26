namespace ByteBuy.Core.Domain.ValueObjects;

public record ImageThumbnail
{
    public string ImagePath { get; init; } = null!;
    public string? AltText { get; init; }
}
