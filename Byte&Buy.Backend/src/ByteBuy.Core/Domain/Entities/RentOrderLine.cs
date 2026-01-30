using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class RentOrderLine : OrderLine
{
    public Money PricePerDay { get; private set; } = null!;
    public int RentalDays { get; private set; }

    public override Money TotalPrice => PricePerDay * RentalDays * Quantity;

    private RentOrderLine() { }

    private RentOrderLine(
    Guid orderId,
    string itemName,
    ImageThumbnail thumbnail,
    int quantity,
    Money pricePerDay,
    int rentalDays) : base(orderId, itemName, thumbnail, quantity)
    {
        PricePerDay = pricePerDay;
        RentalDays  = rentalDays;
    }

    public static Result<RentOrderLine> Create(Guid orderId,
        string itemName,
        string imagePath,
        string? altText,
        int quantity,
        decimal amount,
        string currency,
        int rentalDays)
    {
        if (quantity <= 0)
            return Result.Failure<RentOrderLine>(OrderErrors.QuantityInvalid);

        if (rentalDays <= 0)
            return Result.Failure<RentOrderLine>(OrderErrors.RentalDaysInvalid);

        var moneyResult = Money.Create(amount, currency);
        if (moneyResult.IsFailure)
            return Result.Failure<RentOrderLine>(moneyResult.Error);

        var thumbnailResult = ImageThumbnail.Create(imagePath, altText);
        if (thumbnailResult.IsFailure)
            return Result.Failure<RentOrderLine>(thumbnailResult.Error);

        return new RentOrderLine(orderId, itemName, thumbnailResult.Value, quantity, moneyResult.Value, rentalDays);
    }
}
