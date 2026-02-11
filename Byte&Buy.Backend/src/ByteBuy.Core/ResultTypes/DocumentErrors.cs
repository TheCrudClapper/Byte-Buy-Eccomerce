namespace ByteBuy.Core.ResultTypes;

public static class DocumentErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "OrderDetails", "Order is not found or its status is invalid");
}
