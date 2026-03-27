using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Shared.Errors;

public static class DocumentErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "OrderDetails", "Order is not found or its status is invalid");
}
