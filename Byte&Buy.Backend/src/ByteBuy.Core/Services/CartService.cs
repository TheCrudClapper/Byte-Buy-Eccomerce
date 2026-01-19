using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Cart;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CartSpecifications;

namespace ByteBuy.Core.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly IDeliveryRepository _deliveryRepository;
    public CartService(ICartRepository cartRepository,
        IOfferRepository offerRepository,
        IDeliveryRepository deliveryRepository)
    {
        _cartRepository = cartRepository;
        _offerRepository = offerRepository;
        _deliveryRepository = deliveryRepository;
    }

    public async Task<Result> AddRentCartOffer(Guid userId, RentCartOfferAddRequest request)
    {
        var spec = new CartAggregateWithOffersByUserIdSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure(Error.NotFound);

        var offer = await _offerRepository.GetByIdAsync(request.OfferId);
        if (offer is not RentOffer rentOffer)
            return Result.Failure(Error.NotFound);

        if (offer.CreatedByUserId == userId)
            return Result.Failure(CartErrors.SelfOfferCartAdd);

        var result = cart.AddRentCartOffer(rentOffer, request.Quantity, request.RentalDays);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> AddSaleCartOffer(Guid userId, SaleCartOfferAddRequest request)
    {
        var spec = new CartAggregateWithOffersByUserIdSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure(Error.NotFound);

        var offer = await _offerRepository.GetByIdAsync(request.OfferId);
        if (offer is not SaleOffer saleOffer)
            return Result.Failure(Error.NotFound);

        if (offer.CreatedByUserId == userId)
            return Result.Failure(CartErrors.SelfOfferCartAdd);

        var result = cart.AddSaleCartOffer(saleOffer, request.Quantity);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<CartSummaryResponse>> DeleteCartOffer(Guid userId, Guid cartItemId)
    {
        var spec = new CartAggregateWithOffersByUserIdSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure<CartSummaryResponse>(Error.NotFound);

        var deleteResult = cart.RemoveCartItem(cartItemId);
        if (deleteResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(deleteResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();

        var cartSummaryResult = await CalculateCartSummary(cart);
        if (cartSummaryResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(cartSummaryResult.Error);

        return cartSummaryResult.Value;
    }

    public async Task<Result<CartResponse>> GetCart(Guid userId, CancellationToken ct = default)
    {
        var spec = new CartAggegateWithFullOffer(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec, ct);

        if (cart is null)
            return Result.Failure<CartResponse>(CartErrors.NotFound);

        var cartSummaryResult = await CalculateCartSummary(cart);
        if (cartSummaryResult.IsFailure)
            return Result.Failure<CartResponse>(cartSummaryResult.Error);

        var cartItems = cart.CartOffers
            .Select(c => c.ToCartItemResponse())
            .ToList();

        return new CartResponse(
            cartItems,
            cartSummaryResult.Value);
    }

    public async Task<Result<CartSummaryResponse>> CalculateCartSummary(Cart cart)
    {
        var itemsQuantity = 0;
        foreach (var item in cart.CartOffers)
            itemsQuantity += item.Quantity;

        var offerIds = cart.CartOffers
            .Select(co => co.OfferId)
            .Distinct()
            .ToList();

        //Get cheapest delivery per offer in cart
        //When user has two offers one rental one sell but the same offer -> one delivery cost
        //When user has different offers from one seller -> each offer has its delivery cost
        var cheapestDeliveries = await _deliveryRepository
            .GetCheapestCostByOfferIds(offerIds);

        //one currency used in cart 
        var currency = cart.TotalItemsValue.Currency;

        Money cheapestDeliveriesSum = cheapestDeliveries
            .Aggregate(Money.Zero, (sum, delivery) => sum + delivery);

        var totalCost = cheapestDeliveriesSum + cart.TotalItemsValue;
        var taxValue = totalCost.Multiply(0.23m);

        return new CartSummaryResponse(
            itemsQuantity,
            cart.TotalItemsValue.ToMoneyDto(),
            taxValue.ToMoneyDto(),
            cheapestDeliveriesSum.ToMoneyDto(),
            totalCost.ToMoneyDto());
    }

    public async Task<Result<CartSummaryResponse>> UpdateRentCartOffer(Guid userId, Guid cartItemId, RentCartOfferUpdateRequest request)
    {
        var spec = new CartAggregateWithOffersByUserIdSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure<CartSummaryResponse>(CartErrors.NotFound);

        var updateResult = cart
            .UpdateRentCartOffer(cartItemId, request.Quantity, request.RentalDays);

        if (updateResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(updateResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();

        var cartSummaryResult = await CalculateCartSummary(cart);
        if (cartSummaryResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(cartSummaryResult.Error);

        return cartSummaryResult.Value;
    }

    public async Task<Result<CartSummaryResponse>> UpdateSaleCartOffer(Guid userId, Guid cartItemId, SaleCartOfferUpdateRequest request)
    {
        var spec = new CartAggregateWithOffersByUserIdSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure<CartSummaryResponse>(CartErrors.NotFound);

        var updateResult = cart.UpdateSaleCartOffer(cartItemId, request.Quantity);
        if (updateResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(updateResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();

        var cartSummaryResult = await CalculateCartSummary(cart);
        if (cartSummaryResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(cartSummaryResult.Error);

        return cartSummaryResult.Value;
    }
}
