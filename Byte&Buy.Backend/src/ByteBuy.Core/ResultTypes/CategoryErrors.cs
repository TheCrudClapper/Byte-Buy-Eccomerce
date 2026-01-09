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

    public static readonly Error NameInvalid = Error.Validation(
        "Category.Name", "Name is required and must be at most 20 characters.");

    public static readonly Error DescriptionContentInvalid = Error.Validation(
        "Category.Description", "Description cannot contain only whitespace.");

    public static readonly Error DescriptionLengthInvalid = Error.Validation(
        "Category.Description", "Description must be at most 50 characters.");

}
