using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class RentOffer : Offer
{
    private RentOffer() { }
    public Money PricePerDay { get; set; } = null!;
    public int MaxRentalDays { get; set; }

    public RentOffer(
        Guid itemId,
        Guid createdByUserId,
        int quantityAvailable,
        Money pricePerDay,
        int maxRentalDays,
        AddressValueObject offerAddress) : base(itemId, createdByUserId, quantityAvailable, offerAddress)
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
            return Result.Failure(OfferErrors.MaxRentalDaysInvalid);

        return Result.Success();
    }

    public static Result<RentOffer> Create(Guid itemId,
        Guid createdByUserId,
        int quantityAvailable,
        decimal pricePerDay,
        int maxRentalDays,
        AddressValueObject offerAddress,
        IEnumerable<Guid> deliveriesIds)
    {
        var validationResult = Validate(quantityAvailable, maxRentalDays);
        if (validationResult.IsFailure)
            return Result.Failure<RentOffer>(validationResult.Error);

        var moneyResult = Money.Create(pricePerDay);
        if (moneyResult.IsFailure)
            return Result.Failure<RentOffer>(moneyResult.Error);

        var money = moneyResult.Value;

        var rentOffer = new RentOffer(itemId, createdByUserId, quantityAvailable, money, maxRentalDays, offerAddress);
        rentOffer.AssignDeliveriesToOffer(deliveriesIds);

        return rentOffer;
    }

    public Result Update(
        int quantityAvailable,
        decimal pricePerDay,
        int maxRentalDays,
        IEnumerable<Guid> deliveriesIds)
    {
        var validationResult = Validate(quantityAvailable, maxRentalDays);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        var moneyResult = Money.Create(pricePerDay);
        if (moneyResult.IsFailure)
            return Result.Failure(moneyResult.Error);

        var money = moneyResult.Value;

        PricePerDay = money;
        MaxRentalDays = maxRentalDays;
        QuantityAvailable = quantityAvailable;
        DateEdited = DateTime.UtcNow;

        var deliveryUpdateResult = UpdateDeliveries(deliveriesIds);
        if (deliveryUpdateResult.IsFailure)
            return deliveryUpdateResult;

        return Result.Success();
    }
}
