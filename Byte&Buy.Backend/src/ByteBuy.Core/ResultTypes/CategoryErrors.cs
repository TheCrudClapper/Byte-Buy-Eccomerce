namespace ByteBuy.Core.ResultTypes;

public static class CategoryErrors
{
    public static readonly Error AlreadyExists = new Error
        (400, "Category with this name already exists");

    public static readonly Error NotFound = new Error
        (404, "Category with this Id doesnt exist");

    public static readonly Error InUse = new Error
        (400, "Category is used, cannot be deleted");
}
