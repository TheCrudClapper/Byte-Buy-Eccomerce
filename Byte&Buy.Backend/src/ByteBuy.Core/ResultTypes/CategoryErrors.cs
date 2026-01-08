namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with category aggregate
/// </summary>
public static class CategoryErrors
{
    public static readonly Error AlreadyExists = new(
        ErrorType.Conflict, "Category.AlreadyExists", "Category with this data already exists");

    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Category.NotFound", "Category is not found");

    public static readonly Error HasActiveItems = new(
        ErrorType.Conflict, "Category.HasActiveItems", "Category is actively used, cannot delete");
}
