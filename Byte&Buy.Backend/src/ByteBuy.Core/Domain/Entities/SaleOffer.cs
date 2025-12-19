using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;
namespace ByteBuy.Core.Domain.Entities;

public class SaleOffer : Offer
{
    private SaleOffer() { }
    public Money PricePerItem { get; set; } = null!;

    public SaleOffer(
        Guid itemId,
        Guid createdByUserId,
        int quantityAvailable,
        Money pricePerItem) : base(itemId, createdByUserId, quantityAvailable)
    {
        PricePerItem = pricePerItem;
    }

    public static Result<SaleOffer> Create(Guid itemId,
     Guid createdByUserId,
     int quantityAvailable,
     decimal pricePerItem)
    {
        var validationResult = ValidateBasicInfo(quantityAvailable);
        if (validationResult.IsFailure)
            return Result.Failure<SaleOffer>(validationResult.Error);

        var moneyResult = Money.Create(pricePerItem);
        if (moneyResult.IsFailure)
            return Result.Failure<SaleOffer>(moneyResult.Error);

        var money = moneyResult.Value;

        return new SaleOffer(itemId, createdByUserId, quantityAvailable, money);
    }

    public Result Update(
       Guid itemId,
       int quantityAvailable,
       decimal pricePerDay,
       int maxRentalDays)
    {
        var validationResult = ValidateBasicInfo(quantityAvailable);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        var moneyResult = Money.Create(pricePerDay);
        if (moneyResult.IsFailure)
            return Result.Failure(moneyResult.Error);

        var money = moneyResult.Value;

        ItemId = itemId;
        PricePerItem = money;
        QuantityAvailable = quantityAvailable;
        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

}
