using ByteBuy.Core.Domain.EntityContracts;
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
            return Result.Failure(Error.Validation("Name is required and must be at most 75 characters."));

        if (string.IsNullOrWhiteSpace(description) || description.Length > 2000)
            return Result.Failure(Error.Validation("Description is required and must be at most 2000 characters."));

        if (stockQuantity < 1)
            return Result.Failure(Error.Validation("Quantity can'tbe lower than 1."));

        return Result.Success();
    }

    public static Result<Item> Create(string name, string description, Guid categoryId, Guid conditionId, int stockQuantity)
        => CreateInternal(name, description, categoryId, conditionId, stockQuantity, false);

    public static Result<Item> CreateCompanyItem(string name, string description, Guid categoryId, Guid conditionId, int stockQuantity)
        => CreateInternal(name, description, categoryId, conditionId, stockQuantity, true);

    private static Result<Item> CreateInternal(
    string name,
    string description,
    Guid categoryId,
    Guid conditionId,
    int stockQuantity,
    bool isCompanyItem)
    {
        //if (conditionId is null)
        //    return Result.Failure<Item>(Error.Validation("Condition can't be null"));

        //if (category is null)
        //    return Result.Failure<Item>(Error.Validation("Category can't be null"));

        var validationResult = Validate(name, description, stockQuantity);
        if (validationResult.IsFailure)
            return Result.Failure<Item>(validationResult.Error);

        return new Item(
            name,
            description,
            categoryId,
            conditionId,
            stockQuantity,
            isCompanyItem);
    }

    public Result AddImage(string imagePath, string altText)
    {
        var imageResult = Image.Create(imagePath, altText);
        if(imageResult.IsFailure)
            return imageResult;

        var image = imageResult.Value;
        image.AssignToItem(this);
        Images.Add(image);

        return Result.Success();
    }

    public void AssignImageToItem(Image image)
    {
        image.AssignToItem(this);
        Images.Add(image);
    }

    private void DeactivateAllImages()
    {
        foreach (Image image in Images)
        {
            image.Deactivate();
        }
    }
}
