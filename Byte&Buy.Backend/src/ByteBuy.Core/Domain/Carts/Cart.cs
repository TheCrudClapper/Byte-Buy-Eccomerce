using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Carts.Entities;
using ByteBuy.Core.Domain.Offers;
using ByteBuy.Core.Domain.Offers.Enums;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.Domain.Users;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Carts;

public class Cart : AggregateRoot, ISoftDeletable
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
            return Result.Failure<Cart>(CartErrors.EmptyUserId);

        return Result.Success(new Cart(userId));
    }

    public Result AddSaleCartOffer(SaleOffer saleOffer, int quantity)
    {
        if (saleOffer is null)
            return Result.Failure(CartErrors.NullOffer);

        if (saleOffer.Status == OfferStatus.SoldOut)
            return Result.Failure(CartErrors.OfferIsSoldOut);

        if (quantity <= 0)
            return Result.Failure(CartErrors.QuantityInvalid);

        var existingCartOffer = CartOffers
            .OfType<SaleCartOffer>()
            .FirstOrDefault(co => co.OfferId == saleOffer.Id);

        var currentExistingQuantity = existingCartOffer?.Quantity ?? 0;
        var requestedQuantity = currentExistingQuantity + quantity;

        if (requestedQuantity > saleOffer.QuantityAvailable)
            return Result.Failure(CartErrors.RequestedQuantityTooHigh);

        if (existingCartOffer is null)
        {
            var createResult = SaleCartOffer.Create(Id, saleOffer.Id, quantity);
            if (createResult.IsFailure)
                return Result.Failure(createResult.Error);

            //Used for READONLY purposes
            createResult.Value.AssignOffer(saleOffer);
            CartOffers.Add(createResult.Value);
        }
        else
        {
            var quantityResult = existingCartOffer.SetQuantity(requestedQuantity);
            if (quantityResult.IsFailure)
                return quantityResult;
        }

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    public Result UpdateSaleCartOffer(Guid cartItemId, int quantity)
    {
        var cartOffer = CartOffers
            .OfType<SaleCartOffer>()
            .FirstOrDefault(co => co.Id == cartItemId);

        if (cartOffer is null)
            return Result.Failure(CartErrors.OfferNotInCart);

        var offer = cartOffer.Offer;
        if (offer is null || offer is not SaleOffer saleOffer)
            return Result.Failure(CartErrors.NullOffer);

        //in this case, we delete item from cart
        if (quantity <= 0)
        {
            cartOffer.Deactivate();

            var result = RecalculateTotals();
            if (result.IsFailure) return result;

            return Result.Success();
        }

        if (saleOffer.Status == OfferStatus.SoldOut)
            return Result.Failure(CartErrors.OfferIsSoldOut);

        if (quantity > saleOffer.QuantityAvailable)
            return Result.Failure(CartErrors.RequestedQuantityTooHigh);

        var quantityResult = cartOffer.SetQuantity(quantity);
        if (quantityResult.IsFailure)
            return quantityResult;

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    public Result AddRentCartOffer(RentOffer rentOffer, int quantity, int rentalDays)
    {
        if (rentOffer is null)
            return Result.Failure(CartErrors.NullOffer);

        if (rentOffer.Status == OfferStatus.SoldOut)
            return Result.Failure(CartErrors.OfferIsSoldOut);

        if (quantity <= 0)
            return Result.Failure(CartErrors.QuantityInvalid);

        if (rentalDays <= 0)
            return Result.Failure(CartErrors.RentalDaysInvalid);

        if (rentalDays > rentOffer.MaxRentalDays)
            return Result.Failure(CartErrors.RentalDaysTooHigh);

        var existingCartOffer = CartOffers
            .OfType<RentCartOffer>()
            .FirstOrDefault(co => co.OfferId == rentOffer.Id &&
                            co.RentalDays == rentalDays);

        var totalReservedQuantity = CartOffers
            .OfType<RentCartOffer>()
            .Where(co => co.OfferId == rentOffer.Id)
            .Sum(co => co.Quantity);


        //var currentExistingQuantity = existingCartOffer?.Quantity ?? 0;
        var requestedQuantity = totalReservedQuantity + quantity;

        if (requestedQuantity > rentOffer.QuantityAvailable)
            return Result.Failure(CartErrors.RequestedQuantityTooHigh);

        if (existingCartOffer is null)
        {
            var createResult = RentCartOffer.Create(Id, rentOffer.Id, quantity, rentalDays);
            if (createResult.IsFailure)
                return Result.Failure(createResult.Error);

            //Used for READONLY purposes
            createResult.Value.AssignOffer(rentOffer);
            CartOffers.Add(createResult.Value);
        }
        else
        {
            var changeResult = existingCartOffer
                .ChangeQuantityAndRentalDays(requestedQuantity, rentalDays);

            if (changeResult.IsFailure)
                return changeResult;
        }

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    public Result UpdateRentCartOffer(Guid cartItemId, int quantity, int rentalDays)
    {
        var cartOffer = CartOffers
            .OfType<RentCartOffer>()
            .FirstOrDefault(co => co.Id == cartItemId);

        if (cartOffer is null)
            return Result.Failure(CartErrors.OfferNotInCart);

        var offer = cartOffer.Offer;
        if (offer is null || offer is not RentOffer rentOffer)
            return Result.Failure(CartErrors.NullOffer);

        if (quantity <= 0)
        {
            cartOffer.Deactivate();

            var result = RecalculateTotals();
            if (result.IsFailure) return result;

            return Result.Success();
        }

        if (rentalDays <= 0)
            return Result.Failure(CartErrors.RentalDaysInvalid);

        if (rentalDays > rentOffer.MaxRentalDays)
            return Result.Failure(CartErrors.RentalDaysTooHigh);

        if (rentOffer.Status == OfferStatus.SoldOut)
            return Result.Failure(CartErrors.OfferIsSoldOut);

        if (quantity > rentOffer.QuantityAvailable)
            return Result.Failure(CartErrors.RequestedQuantityTooHigh);

        var changeResult = cartOffer
               .ChangeQuantityAndRentalDays(quantity, rentalDays);

        if (changeResult.IsFailure)
            return changeResult;

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    public void ClearCart()
    {
        foreach (var cartOffer in CartOffers)
            cartOffer.Deactivate();

        TotalCartValue = Money.Zero;
        TotalItemsValue = Money.Zero;
        DateEdited = DateTime.UtcNow;
    }

    public Result RemoveCartItem(Guid cartItemId)
    {
        var cartItem = CartOffers.FirstOrDefault(item => item.Id == cartItemId);
        if (cartItem is null)
            return Result.Failure(CartErrors.OfferNotInCart);

        cartItem.Deactivate();

        var totalsResult = RecalculateTotals();
        if (totalsResult.IsFailure)
            return totalsResult;

        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    private Result RecalculateTotals()
    {
        Money ItemsTotal = Money.Zero;
        var activeCartOffers = CartOffers
            .Where(cartOffer => cartOffer.IsActive)
            .ToList();

        foreach (var co in activeCartOffers)
        {
            if (co is RentCartOffer rent)
            {
                var rentOffer = (RentOffer)co.Offer;
                ItemsTotal += co.Quantity * rent.RentalDays * rentOffer.PricePerDay;
            }
            if (co is SaleCartOffer sale)
            {
                var saleOffer = (SaleOffer)co.Offer;
                ItemsTotal += co.Quantity * saleOffer.PricePerItem;
            }
        }

        TotalCartValue = ItemsTotal.Copy();
        TotalItemsValue = ItemsTotal.Copy();

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


