using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

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
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    public bool IsCompanyItem { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    private Item() { }

    private Item(string name, string description, Guid categoryId, Guid conditionId, int stockQuantity, bool isCompanyItem)
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        ConditionId = conditionId;
        StockQuantity = stockQuantity;
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

    public static Result Validate(string name, string description, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 75)
            return Result.Failure(ItemErrors.NameInvalid);

        if (string.IsNullOrWhiteSpace(description) || description.Length > 2000)
            return Result.Failure(ItemErrors.DescriptionInvalid);

        if (stockQuantity < 1)
            return Result.Failure(ItemErrors.StockQuantityInvalid);

        return Result.Success();
    }

    public static Result<Item> Create(string name, string description, Guid categoryId, Guid conditionId, int stockQuantity, IList<ImageDraft> images)
        => CreateInternal(name, description, categoryId, conditionId, stockQuantity, images, false);

    public static Result<Item> CreateCompanyItem(string name, string description, Guid categoryId, Guid conditionId, int stockQuantity, IList<ImageDraft> images)
        => CreateInternal(name, description, categoryId, conditionId, stockQuantity, images, true);

    private static Result<Item> CreateInternal(
    string name,
    string description,
    Guid categoryId,
    Guid conditionId,
    int stockQuantity,
    IList<ImageDraft> images,
    bool isCompanyItem)
    {

        var validationResult = Validate(name, description, stockQuantity);
        if (validationResult.IsFailure)
            return Result.Failure<Item>(validationResult.Error);

        var item = new Item(
           name,
           description,
           categoryId,
           conditionId,
           stockQuantity,
           isCompanyItem);

        foreach (var image in images)
        {
            var imageAddResult = item.AddImage(image.ImagePath, image.AltText);
            if (imageAddResult.IsFailure)
                return Result.Failure<Item>(imageAddResult.Error);
        }

        return item;
    }

    public Result Update(string name, string description, Guid categoryId, Guid conditionId, int stockQuantity)
    {
        var validationResult = Validate(name, description, stockQuantity);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        Name = name;
        Description = description;
        CategoryId = categoryId;
        ConditionId = conditionId;
        StockQuantity = stockQuantity;

        return Result.Success();
    }

    public void DeleteImagesById(Guid imageId)
    {
        var image = Images.FirstOrDefault(i => i.Id == imageId);
        image?.Deactivate();
    }

    public Result ChangeImageAltText(Guid imageId, string altText)
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
        if (quantity < 1)
            return Result.Failure(ItemErrors.StockQuantityInvalid);

        if (quantity > StockQuantity)
            return Result.Failure(ItemErrors.StockNotEnough);

        StockQuantity -= quantity;
        return Result.Success();
    }

    public Result AddStock(int quantity)
    {
        if (quantity < 1)
            return Result.Failure(ItemErrors.StockQuantityInvalid);

        StockQuantity += quantity;
        return Result.Success();
    }

    public Result AddImage(string imagePath, string altText)
    {
        var imageResult = Image.Create(imagePath, altText);
        if (imageResult.IsFailure)
            return imageResult;

        var image = imageResult.Value;
        image.AssignToItem(this);
        Images.Add(image);

        return Result.Success();
    }

    public IList<string> GetImagePathsByIds(IList<Guid> ids)
    {
        var paths = new List<string>();
        return Images.Where(img => ids.Contains(img.Id))
            .Select(img => img.ImagePath)
            .ToList();
    }

    private void DeactivateAllImages()
    {
        foreach (Image image in Images)
            image.Deactivate();
    }
}
