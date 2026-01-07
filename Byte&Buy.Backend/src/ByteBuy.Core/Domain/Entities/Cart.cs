using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;
using System;

namespace ByteBuy.Core.Domain.Entities;

public class Cart : AuditableEntity, ISoftDeletable
{
    public Guid UserId { get; private set; }
    public Money TotalCartValue { get; private set; } = Money.Zero;
    public Money TotalItemsValue { get; private set; } = Money.Zero;
    public ICollection<CartOffer> CartOffers { get; private set; } = new List<CartOffer>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }


    //EF Navigation Property ONLY
    public PortalUser User { get; private set; } = null!;

    private Cart() { }
    private Cart(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TotalCartValue = Money.Zero;
        TotalItemsValue = Money.Zero;
        DateCreated = DateTime.UtcNow;
        IsActive = true;
    }

    public static Result<Cart> Create(Guid userId)
    {
        if (userId == Guid.Empty)
            return Result.Failure<Cart>(Error.Validation("UserId cannot be empty"));

        return Result.Success(new Cart(userId));
    }

    public Result AddSaleCartOffer(SaleOffer saleOffer, int quantity)
    {
        if (saleOffer is null)
            return Result.Failure(Error.Validation("Offer can't be null"));

        if (quantity <= 0)
            return Result.Failure(Error.Validation("Quantity must be greater than 0."));

        var existingCartOffer = CartOffers
            .OfType<SaleCartOffer>()
            .FirstOrDefault(co => co.OfferId == saleOffer.Id);

        var currentExistingQuantity = existingCartOffer?.Quantity ?? 0;
        var requestedQuantity = currentExistingQuantity + quantity;

        if (requestedQuantity > saleOffer.QuantityAvailable)
            return Result.Failure(Error.Validation("Requested quantity exceedes avaliable quantity !"));

        if (existingCartOffer is null)
        {
            var createResult = SaleCartOffer.Create(Id, saleOffer.Id, quantity);
            if (createResult.IsFailure)
                return Result.Failure(createResult.Error);

            CartOffers.Add(createResult.Value);
        }
        else
        {
            existingCartOffer.Quantity = requestedQuantity;
            existingCartOffer.DateEdited = DateTime.UtcNow;
        }

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        return Result.Success();
    }

    public Result UpdateSaleCartOffer(SaleOffer saleOffer, Guid cartItemId, int quantity)
    {
        if (saleOffer is null)
            return Result.Failure(Error.Validation("Offer can't be null"));

        var cartOffer = CartOffers
            .OfType<SaleCartOffer>()
            .FirstOrDefault(co => co.Id == cartItemId);

        if (cartOffer is null)
            return Result.Failure(Error.Validation("Given offer doesn't exists in your cart"));

        //in this case, we delete item from cart
        if (quantity <= 0)
        {
            cartOffer.Deactivate();

            var result = RecalculateTotals();
            if(result.IsFailure) return result;

            return Result.Success();
        }

        if(quantity > saleOffer.QuantityAvailable)
            return Result.Failure(Error.Validation("Requested quantity exceedes avaliable quantity !"));

        cartOffer.Quantity = quantity;
        cartOffer.DateEdited = DateTime.UtcNow;

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        return Result.Success();
    }

    public Result AddRentCartOffer(RentOffer rentOffer, int quantity, int rentalDays)
    {
        if (rentOffer is null)
            return Result.Failure(Error.Validation("Offer can't be null"));

        if (quantity <= 0)
            return Result.Failure(Error.Validation("Quantity must be greater than 0."));

        if (rentalDays <= 0)
            return Result.Failure(Error.Validation("Rental period must have at least one day !"));

        if (rentalDays > rentOffer.MaxRentalDays)
            return Result.Failure(Error.Validation("You can't rent item for longer than avaliable!"));

        var existingCartOffer = CartOffers
            .OfType<RentCartOffer>()
            .FirstOrDefault(co => co.OfferId == rentOffer.Id &&
                            co.RentalDays == rentalDays);

        var currentExistingQuantity = existingCartOffer?.Quantity ?? 0;
        var requestedQuantity = currentExistingQuantity + quantity;

        if (requestedQuantity > rentOffer.QuantityAvailable)
            return Result.Failure(Error.Validation("Requested quantity exceedes avaliable quantity !"));

        if (existingCartOffer is null)
        {
            var createResult = RentCartOffer.Create(Id, rentOffer.Id, quantity, rentalDays);
            if (createResult.IsFailure)
                return Result.Failure(createResult.Error);

            CartOffers.Add(createResult.Value);
        }
        else
        {
            existingCartOffer.DateEdited = DateTime.UtcNow;
            existingCartOffer.Quantity = requestedQuantity;
            existingCartOffer.RentalDays = rentalDays;
        }

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        return Result.Success();
    }

    public Result UpdateRentCartOffer(RentOffer rentOffer, Guid cartItemId, int quantity, int rentalDays)
    {
        if(rentOffer is null)
            return Result.Failure(Error.Validation("Offer can't be null"));

        var cartOffer = CartOffers
            .OfType<RentCartOffer>()
            .FirstOrDefault(co => co.Id == cartItemId);

        if(cartOffer is null)
            return Result.Failure(Error.Validation("Given offer doesn't exists in your cart"));

        if (quantity <= 0)
        {
            cartOffer.Deactivate();

            var result = RecalculateTotals();
            if (result.IsFailure) return result;

            return Result.Success();
        }

        if (rentalDays <= 0 || rentalDays > rentOffer.MaxRentalDays)
            return Result.Failure(Error
                .Validation($"Rental period must have at least one day but not more than {rentOffer.MaxRentalDays}"));

        if (quantity > rentOffer.QuantityAvailable)
            return Result.Failure(Error.Validation("Requested quantity exceedes avaliable quantity !"));

        cartOffer.Quantity = quantity;
        cartOffer.RentalDays = rentalDays;
        cartOffer.DateEdited = DateTime.UtcNow;

        var totalsResult =  RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        return Result.Success();
    }

    public Result RemoveCartItem(Guid cartItemId)
    {
        var cartItem = CartOffers.FirstOrDefault(item => item.Id == cartItemId);
        if (cartItem is null)
            return Result.Failure(Error.NotFound);

        cartItem.Deactivate();

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        return Result.Success();
    }

    public Result RecalculateTotals()
    {
        decimal itemsTotal = 0;

        foreach (var co in CartOffers)
        {
            if (co is RentCartOffer rent)
            {
                var rentOffer = (RentOffer)co.Offer;
                itemsTotal += co.Quantity * rent.RentalDays * rentOffer.PricePerDay.Amount;
            }
            if (co is SaleCartOffer sale)
            {
                var saleOffer = (SaleOffer)co.Offer;
                itemsTotal += co.Quantity * saleOffer.PricePerItem.Amount;
            }
        }

        var moneyResult = Money.Create(itemsTotal);
        if (moneyResult.IsFailure)
            return moneyResult;

        TotalCartValue = moneyResult.Value;
        TotalItemsValue = moneyResult.Value;

        return Result.Success();
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;

        foreach (var item in CartOffers)
            item.Deactivate();
    }
}


