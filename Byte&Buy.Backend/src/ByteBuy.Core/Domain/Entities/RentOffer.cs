using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class RentOffer : Offer
{
    public Money PricePerDay { get; set; } = null!;
    public int MaxRentalDays { get; set; }

    public RentOffer(
        Guid itemId,
        Guid createdByUserId,
        int quantityAvailable,
        Money pricePerDay,
        int maxRentalDays) : base(itemId, createdByUserId, quantityAvailable)
    {
        PricePerDay = pricePerDay;
        MaxRentalDays = maxRentalDays;
    }

    public static Result Validate(int quantityAvailable, int maxRentalDays)
    {
        var basicValidation = ValidateBasicInfo(quantityAvailable);
        if (basicValidation.IsFailure)
            return Result.Failure(basicValidation.Error);

        if (maxRentalDays < 1)
            return Result.Failure(Error.Validation("Max rental days must be higher than 0"));

        return Result.Success();
    }

    public static Result<RentOffer> Create(Guid itemId,
        Guid createdByUserId,
        int quantityAvailable,
        decimal pricePerDay,
        int maxRentalDays)
    {
        var validationResult = Validate(quantityAvailable, maxRentalDays);
        if (validationResult.IsFailure)
            return Result.Failure<RentOffer>(validationResult.Error);

        var moneyResult = Money.Create(pricePerDay);
        if (moneyResult.IsFailure)
            return Result.Failure<RentOffer>(moneyResult.Error);

        var money = moneyResult.Value;

        return new RentOffer(itemId, createdByUserId, quantityAvailable, money, maxRentalDays);
    }

    public Result Update(
        Guid itemId,
        int quantityAvailable,
        decimal pricePerDay,
        int maxRentalDays)
    {
        var validationResult = Validate(quantityAvailable, maxRentalDays);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        var moneyResult = Money.Create(pricePerDay);
        if (moneyResult.IsFailure)
            return Result.Failure(moneyResult.Error);

        var money = moneyResult.Value;

        ItemId = itemId;
        PricePerDay = money;
        MaxRentalDays = maxRentalDays;
        QuantityAvailable = quantityAvailable;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }
}
