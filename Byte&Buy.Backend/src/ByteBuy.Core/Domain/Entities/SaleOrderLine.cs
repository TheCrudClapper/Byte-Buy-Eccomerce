using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

// Represents singular order item of sale type
public class SaleOrderLine : OrderLine
{
    public Money PricePerItem { get; private set; } = null!;

    private SaleOrderLine() { }

    private SaleOrderLine(
        Guid orderId,
        string itemName,
        ImageThumbnail thumbnail,
        int quantity,
        Money pricePerItem) : base(orderId, itemName, thumbnail, quantity)
    {
        PricePerItem = pricePerItem;
    }

    public static Result<SaleOrderLine> Create(Guid orderId,
        string itemName,
        string imagePath,
        string? altText,
        int quantity,
        int amount,
        string currency)
    {
        if (quantity <= 0)
            return Result.Failure<SaleOrderLine>(OrderErrors.QuantityInvalid);

        var moneyResult = Money.Create(amount, currency);
        if (moneyResult.IsFailure)
            return Result.Failure<SaleOrderLine>(moneyResult.Error);

        var thumbnailResult = ImageThumbnail.Create(imagePath, altText);
        if (thumbnailResult.IsFailure)
            return Result.Failure<SaleOrderLine>(thumbnailResult.Error);

        return new SaleOrderLine(orderId, itemName, thumbnailResult.Value, quantity, moneyResult.Value);
    }
}
