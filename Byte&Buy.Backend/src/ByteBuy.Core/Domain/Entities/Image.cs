using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Image : AuditableEntity, ISoftDeletable
{
    public string ImagePath { get; private set; } = null!;
    public Guid ItemId { get; private set; }
    public Item Item { get; private set; } = null!;
    public string AltText { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }

    private Image() { }
    private Image(string imagePath, string altText)
    {
        ImagePath = imagePath;
        AltText = altText;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public Result ChangeImageAltText(string altText)
    {
        var validationResult = Validate(altText);
        if (validationResult.IsFailure)
            return Result.Failure<Image>(validationResult.Error);

        AltText = altText;
        return Result.Success();
    }

    private static Result Validate(string altText)
    {
        if (string.IsNullOrWhiteSpace(altText) || altText.Length > 50)
            return Result.Failure(Error.Validation("Alernative Text is required and must be at most 50 characters."));

        return Result.Success();
    }

    internal static Result<Image> Create(string imagePath, string altText)
    {
        var validationResult = Validate(altText);
        if (validationResult.IsFailure)
            return Result.Failure<Image>(validationResult.Error);

        if (string.IsNullOrWhiteSpace(imagePath))
            return Result.Failure<Image>(Error.Validation("Image Path is required"));

        return new Image(imagePath, altText);
    }

    internal void AssignToItem(Item item)
    {
        ItemId = item.Id;
        Item = item;
    }
}
