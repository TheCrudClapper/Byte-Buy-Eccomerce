using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.ValueObjects;

// Value Object that represents image thumbnail 
public record ImageThumbnail
{
    public string ImagePath { get; private set; } = null!;
    public string? AltText { get; private set; }

    private ImageThumbnail() { }
    private ImageThumbnail(string imagePath, string? altText)
    {
        ImagePath = imagePath;
        AltText = altText;
    }

    public static Result<ImageThumbnail> Create(string imagePath, string? altText)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return Result.Failure<ImageThumbnail>(ImageErrors.ImagePathInvalid);

        if (!string.IsNullOrEmpty(altText))
        {
            if (altText.Length > 50)
                return Result.Failure<ImageThumbnail>(ImageErrors.AltTextInvalid);
        }

        return new ImageThumbnail(imagePath, altText);
    }
}
