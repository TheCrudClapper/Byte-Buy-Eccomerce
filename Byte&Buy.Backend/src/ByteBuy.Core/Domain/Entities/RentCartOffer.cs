using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class RentCartOffer : CartOffer
{
    public int RentalDays { get; set; }

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

    public Result ChangeRentalDays(int rentalDays)
    {
        if (rentalDays <= 0)
            return Result.Failure<RentCartOffer>(CartErrors.RentalDaysInvalid);

        RentalDays = rentalDays;
        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }
}
