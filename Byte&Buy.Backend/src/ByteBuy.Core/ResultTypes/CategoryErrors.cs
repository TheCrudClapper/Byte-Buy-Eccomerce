namespace ByteBuy.Core.ResultTypes;

public static class CategoryErrors
{
    public static readonly Error AlreadyExists = new Error
        (400, "Category with this name already exists");
}
