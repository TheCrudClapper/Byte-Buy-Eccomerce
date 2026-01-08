using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class SaleCartOffer : CartOffer
{
    private SaleCartOffer() { }
    private SaleCartOffer(Guid cartId, Guid offerId, int quantity)
      : base(cartId, offerId, quantity) { }

    public static Result<SaleCartOffer> Create(Guid cartId, Guid offerId, int quantity)
    {
        var validateResult = Validate(quantity);
        if (validateResult.IsFailure)
            return Result.Failure<SaleCartOffer>(validateResult.Error);

        return new SaleCartOffer(cartId, offerId, quantity);
    }
}
