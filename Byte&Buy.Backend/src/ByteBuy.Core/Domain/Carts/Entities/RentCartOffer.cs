using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Carts.Entities;

public class RentCartOffer : CartOffer
{
    public int RentalDays { get; private set; }

    private RentCartOffer() { }
    private RentCartOffer(Guid cartId, Guid offerId, int quantity, int rentalDays)
        : base(cartId, offerId, quantity)
    {
        RentalDays = rentalDays;
    }

    public static Result<RentCartOffer> Create(Guid cartId, Guid offerId, int quantity, int rentalDays)
    {
        var validateResult = Validate(quantity);
        if (validateResult.IsFailure)
            return Result.Failure<RentCartOffer>(validateResult.Error);

        if (rentalDays <= 0)
            return Result.Failure<RentCartOffer>(CartErrors.RentalDaysInvalid);

        return new RentCartOffer(cartId, offerId, quantity, rentalDays);
    }

    //Method allows for quantity and rental days change
    public Result ChangeQuantityAndRentalDays(int quantity, int rentalDays)
    {
        var quantityResult = SetQuantity(quantity);
        if (quantityResult.IsFailure)
            return quantityResult;

        var rentalDaysResult = ChangeRentalDays(rentalDays);
        if (rentalDaysResult.IsFailure)
            return rentalDaysResult;

        return Result.Success();
    }

    public Result ChangeRentalDays(int rentalDays)
    {
        if (rentalDays <= 0)
            return Result.Failure(CartErrors.RentalDaysInvalid);

        RentalDays = rentalDays;
        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }
}
