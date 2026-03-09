using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.DomainModels;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

/// <summary>
/// Item is an aggreagte that has list of image entities. Item spans two business contexts
/// 
/// A - Item in context of company is an warehouse resource, that company workers can manage,
/// add, substract, delete. It exposes an api to manipulate stock - that is only avaliable when
/// item IsCompanyItem.
/// 
/// B - Item in context of user represents basic information about given offer.
/// 
/// </summary>
public class Item : AuditableEntity, ISoftDeletable
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid ConditionId { get; set; }
    public Condition Condition { get; set; } = null!;
    public int StockQuantity { get; set; }
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public bool IsCompanyItem { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    //EF Navigation Only
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    private Item() { }

    private Item(string name, string description, Guid categoryId, Guid conditionId, bool isCompanyItem, int stockQuantity = 0)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CategoryId = categoryId;
        ConditionId = conditionId;
        StockQuantity = isCompanyItem ? stockQuantity : 0;
        IsActive = true;
        IsCompanyItem = isCompanyItem;
        DateCreated = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
        DeactivateAllImages();
    }

    private static Result ValidateCommon(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 75)
            return Result.Failure(ItemErrors.NameInvalid);

        if (string.IsNullOrWhiteSpace(description) || description.Length > 2000)
            return Result.Failure(ItemErrors.DescriptionInvalid);

        return Result.Success();
    }

    //Static factory method that creates item in user context
    public static Result<Item> CreateUserItem(string name, string description, Guid categoryId, Guid conditionId, IEnumerable<ImageDraft> images)
    {
        var validationResult = ValidateCommon(name, description);
        if (validationResult.IsFailure)
            return Result.Failure<Item>(validationResult.Error);

        return CreateInternal(name, description, categoryId, conditionId, 0, images, false);
    }

    //Static factory method that creates item in company context
    public static Result<Item> CreateCompanyItem(string name, string description, Guid categoryId, Guid conditionId, int stockQuantity, IEnumerable<ImageDraft> images)
    {
        if (stockQuantity < 1)
            return Result.Failure<Item>(ItemErrors.StockQuantityInvalid);

        var validationResult = ValidateCommon(name, description);
        if (validationResult.IsFailure)
            return Result.Failure<Item>(validationResult.Error);

        return CreateInternal(name, description, categoryId, conditionId, stockQuantity, images, true);
    }

    private static Result<Item> CreateInternal(
    string name,
    string description,
    Guid categoryId,
    Guid conditionId,
    int stockQuantity,
    IEnumerable<ImageDraft> images,
    bool isCompanyItem)
    {
        var item = new Item(
           name,
           description,
           categoryId,
           conditionId,
           isCompanyItem,
           stockQuantity);

        if (!images.Any())
            return Result.Failure<Item>(ItemErrors.ImageRequired);

        foreach (var image in images)
        {
            var imageAddResult = item.AddImage(image.ImagePath, image.AltText);
            if (imageAddResult.IsFailure)
                return Result.Failure<Item>(imageAddResult.Error);
        }

        return item;
    }

    public Result UpdateUserItem(
        string name,
        string description,
        Guid categoryId,
        Guid conditionId,
        IEnumerable<ImageDraft>? newImages,
        IEnumerable<ExistingImageUpdate> existingImages)
    {
        var validationResult = ValidateCommon(name, description);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        return UpdateInternal(name, description, categoryId, conditionId, 0, newImages, existingImages);
    }

    public Result UpdateCompanyItem(
        string name,
        string description,
        Guid categoryId,
        Guid conditionId,
        int additionalStockQuantity,
        IEnumerable<ImageDraft>? newImages,
        IEnumerable<ExistingImageUpdate> existingImages)
    {
        if (additionalStockQuantity < 0)
            return Result.Failure(ItemErrors.AdditionalStockUpdateQuantityInvalid);

        var validationResult = ValidateCommon(name, description);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        return UpdateInternal(name, description, categoryId, conditionId, additionalStockQuantity, newImages, existingImages);
    }


    private Result UpdateInternal(
        string name,
        string description,
        Guid categoryId,
        Guid conditionId,
        int additionalStockQuantity,
        IEnumerable<ImageDraft>? newImages,
        IEnumerable<ExistingImageUpdate> existingImages)
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        ConditionId = conditionId;
        StockQuantity += additionalStockQuantity;

        if (newImages is not null && newImages.Any())
        {
            foreach (var image in newImages)
            {
                var imageAddResult = AddImage(image.ImagePath, image.AltText);
                if (imageAddResult.IsFailure)
                    return Result.Failure<Item>(imageAddResult.Error);
            }
        }

        var imageResult = UpdateOrMarkAsDeletedExistingImages(existingImages);
        if (imageResult.IsFailure)
            return imageResult;

        if (!HasAtLeastOneActiveImage())
            return Result.Failure(ItemErrors.ImageRequired);

        return Result.Success();
    }

    // deletes image when marked as deleted or change their metadata
    private Result UpdateOrMarkAsDeletedExistingImages(IEnumerable<ExistingImageUpdate> existingImages)
    {
        foreach (var existing in existingImages)
        {
            var img = Images.FirstOrDefault(i => i.Id == existing.Id);
            if (img is null) continue;

            if (existing.IsDeleted)
            {
                img.Deactivate();
            }
            else
            {
                var changeResult = ChangeImageAltText(existing.Id, existing.AltText);
                if (changeResult.IsFailure)
                    return Result.Failure(changeResult.Error);
            }
        }
        return Result.Success();
    }

    public Result ChangeImageAltText(Guid imageId, string? altText)
    {
        var image = Images.FirstOrDefault(i => i.Id == imageId);
        if (image is null)
            return Result.Failure(ItemErrors.ImageNotFound);

        var altTextResult = image.ChangeImageAltText(altText);
        if (altTextResult.IsFailure)
            return Result.Failure(altTextResult.Error);

        return Result.Success();
    }

    public Result SubstractStock(int quantity)
    {
        if (!IsCompanyItem)
            return Result.Failure(ItemErrors.StockNotSupported);

        if (quantity < 1)
            return Result.Failure(ItemErrors.StockQuantityInvalid);

        if (quantity > StockQuantity)
            return Result.Failure(ItemErrors.StockNotEnough);

        StockQuantity -= quantity;
        return Result.Success();
    }

    public Result AddStock(int quantity)
    {
        if (!IsCompanyItem)
            return Result.Failure(ItemErrors.StockNotSupported);

        if (quantity < 1)
            return Result.Failure(ItemErrors.StockQuantityInvalid);

        StockQuantity += quantity;
        return Result.Success();
    }

    public Result AddImage(string imagePath, string? altText)
    {
        var imageResult = Image.Create(imagePath, altText);
        if (imageResult.IsFailure)
            return imageResult;

        var image = imageResult.Value;
        image.AssignToItem(this);
        Images.Add(image);

        return Result.Success();
    }

    public IReadOnlyList<string> GetImagePathsByIds(IList<Guid> ids)
    {
        return Images.Where(img => ids.Contains(img.Id))
            .Select(img => img.ImagePath)
            .ToList();
    }

    private bool HasAtLeastOneActiveImage()
        => Images.Any(img => img.IsActive);

    private void DeactivateAllImages()
    {
        foreach (Image image in Images)
            image.Deactivate();
    }
}
