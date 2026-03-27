using ByteBuy.Core.Domain.Offers.Base;
using ByteBuy.Core.Domain.Offers.Errors;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Shared.ValueObjects;

namespace ByteBuy.Core.Domain.Offers;

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
        AddressValueObject offerAddress,
        Seller seller) : base(itemId, createdByUserId, quantityAvailable, offerAddress, seller)
    {
        PricePerDay = pricePerDay;
        MaxRentalDays = maxRentalDays;
    }

    public static Result ValidateCreate(int quantityAvailable, int maxRentalDays)
    {
        var basicValidation = ValidateBasicCreateData(quantityAvailable);
        if (basicValidation.IsFailure)
            return Result.Failure(basicValidation.Error);

        if (maxRentalDays < 1 || maxRentalDays > 360)
            return Result.Failure(OfferErrors.MaxRentalDaysInvalid);

        return Result.Success();
    }

    public static Result<RentOffer> Create(Guid itemId,
        Guid createdByUserId,
        int quantityAvailable,
        decimal pricePerDay,
        int maxRentalDays,
        AddressValueObject offerAddress,
        Seller seller,
        IEnumerable<Guid> deliveriesIds)
    {
        var validationResult = ValidateCreate(quantityAvailable, maxRentalDays);
        if (validationResult.IsFailure)
            return Result.Failure<RentOffer>(validationResult.Error);

        var moneyResult = Money.Create(pricePerDay);
        if (moneyResult.IsFailure)
            return Result.Failure<RentOffer>(moneyResult.Error);

        var money = moneyResult.Value;

        var rentOffer = new RentOffer(itemId, createdByUserId, quantityAvailable, money, maxRentalDays, offerAddress, seller);
        rentOffer.AssignDeliveriesToOffer(deliveriesIds);

        return rentOffer;
    }

    public Result Update(
        int additionalQuantity,
        decimal pricePerDay,
        int currentMaxRentalDays,
        int additionalRentalDays,
        IEnumerable<Guid> deliveriesIds)
    {
        if (additionalQuantity < 0)
            return Result.Failure(OfferErrors.InvalidAdditionalQuantity);

        if (additionalRentalDays < 0)
            return Result.Failure(OfferErrors.InvalidAdditionalRentalDays);

        if (additionalRentalDays + currentMaxRentalDays > 360)
            return Result.Failure(OfferErrors.InvalidRentalDaysSum);

        var moneyResult = Money.Create(pricePerDay);
        if (moneyResult.IsFailure)
            return Result.Failure(moneyResult.Error);

        var money = moneyResult.Value;

        PricePerDay = money;
        MaxRentalDays += additionalRentalDays;
        QuantityAvailable += additionalQuantity;
        DateEdited = DateTime.UtcNow;

        MarkAsAvailable();
        var deliveryUpdateResult = UpdateDeliveries(deliveriesIds);
        if (deliveryUpdateResult.IsFailure)
            return deliveryUpdateResult;

        return Result.Success();
    }
}
