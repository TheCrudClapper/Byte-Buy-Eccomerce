using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Orders.Entities;

// Represents singular order item of sale type
public class SaleOrderLine : OrderLine
{
    public Money PricePerItem { get; private set; } = null!;
    public override Money TotalPrice => PricePerItem * Quantity;
    private SaleOrderLine() { }

    private SaleOrderLine(
        Guid orderId,
        Guid offerId,
        string itemName,
        ImageThumbnail thumbnail,
        int quantity,
        Money pricePerItem) : base(orderId, offerId, itemName, thumbnail, quantity)
    {
        PricePerItem = pricePerItem;
    }

    public static Result<SaleOrderLine> Create(Guid orderId,
        Guid offerId,
        string itemName,
        string imagePath,
        string? altText,
        int quantity,
        decimal amount,
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

        return new SaleOrderLine(orderId, offerId, itemName, thumbnailResult.Value, quantity, moneyResult.Value);
    }
}
