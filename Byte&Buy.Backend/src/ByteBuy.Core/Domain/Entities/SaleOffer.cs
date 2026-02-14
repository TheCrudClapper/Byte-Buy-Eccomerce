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
        Money pricePerItem,
        AddressValueObject offerAddress,
        Seller seller) : base(itemId, createdByUserId, quantityAvailable, offerAddress, seller)
    {
        PricePerItem = pricePerItem;
    }

    public static Result<SaleOffer> Create(Guid itemId,
     Guid createdByUserId,
     int quantityAvailable,
     decimal pricePerItem,
     AddressValueObject offerAddress,
     Seller seller,
     IEnumerable<Guid> deliveriesIds)
    {
        var validationResult = ValidateBasicCreateData(quantityAvailable);
        if (validationResult.IsFailure)
            return Result.Failure<SaleOffer>(validationResult.Error);

        var moneyResult = Money.Create(pricePerItem);
        if (moneyResult.IsFailure)
            return Result.Failure<SaleOffer>(moneyResult.Error);

        var money = moneyResult.Value;

        var saleOffer = new SaleOffer(itemId, createdByUserId, quantityAvailable, money, offerAddress, seller);
        saleOffer.AssignDeliveriesToOffer(deliveriesIds);

        return saleOffer;
    }

    public Result Update(
       int additionalQuantity,
       decimal pricePerItem,
       IEnumerable<Guid> deliveriesIds)
    {
        if (additionalQuantity < 0)
            return Result.Failure(OfferErrors.InvalidAdditionalQuantity);

        var moneyResult = Money.Create(pricePerItem);
        if (moneyResult.IsFailure)
            return Result.Failure(moneyResult.Error);

        var money = moneyResult.Value;

        PricePerItem = money;
        QuantityAvailable += additionalQuantity;
        DateEdited = DateTime.UtcNow;

        MarkAsAvailable();

        var deliveryUpdateResult = UpdateDeliveries(deliveriesIds);
        if (deliveryUpdateResult.IsFailure)
            return deliveryUpdateResult;

        return Result.Success();
    }

}
