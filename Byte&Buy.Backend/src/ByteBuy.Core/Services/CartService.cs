using ByteBuy.Core.Domain.Carts;
using ByteBuy.Core.Domain.Offers.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.DTO.Public.Cart;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CartSpecifications;

namespace ByteBuy.Core.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDeliveryRepository _deliveryRepository;
    public CartService(ICartRepository cartRepository,
        IOfferRepository offerRepository,
        IDeliveryRepository deliveryRepository,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _offerRepository = offerRepository;
        _deliveryRepository = deliveryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> AddRentCartOfferAsync(Guid userId, RentCartOfferAddRequest request)
    {
        var spec = new UserCartAggregateSpec(userId, true);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure(CartErrors.NotFound);

        var offer = await _offerRepository.GetByIdAsync(request.OfferId);
        if (offer is not RentOffer rentOffer)
            return Result.Failure(CartErrors.NullOffer);

        if (offer.CreatedByUserId == userId)
            return Result.Failure(CartErrors.SelfOfferCartAdd);

        var result = cart.AddRentCartOffer(rentOffer, request.Quantity, request.RentalDays);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        await _cartRepository.UpdateAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> AddSaleCartOfferAsync(Guid userId, SaleCartOfferAddRequest request)
    {
        var spec = new UserCartAggregateSpec(userId, true);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure(CartErrors.NotFound);

        var offer = await _offerRepository.GetByIdAsync(request.OfferId);
        if (offer is not SaleOffer saleOffer)
            return Result.Failure(CartErrors.NullOffer);

        if (offer.CreatedByUserId == userId)
            return Result.Failure(CartErrors.SelfOfferCartAdd);

        var result = cart.AddSaleCartOffer(saleOffer, request.Quantity);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        await _cartRepository.UpdateAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<CartSummaryResponse>> DeleteCartOfferAsync(Guid userId, Guid cartItemId)
    {
        var spec = new UserCartAggregateAndOffersSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure<CartSummaryResponse>(Error.NotFound);

        var deleteResult = cart.RemoveCartItem(cartItemId);
        if (deleteResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(deleteResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        var cartSummaryResult = await CalculateCartSummary(cart);
        if (cartSummaryResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(cartSummaryResult.Error);

        return cartSummaryResult.Value;
    }

    public async Task<Result<CartResponse>> GetCartAsync(Guid userId, CancellationToken ct = default)
    {
        var spec = new UserCartAggegateWithOffersAggregateSpec(userId);
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

    private async Task<Result<CartSummaryResponse>> CalculateCartSummary(Cart cart)
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

    public async Task<Result<CartSummaryResponse>> UpdateRentCartOfferAsync(Guid userId, Guid cartItemId, RentCartOfferUpdateRequest request)
    {
        var spec = new UserCartAggregateAndOffersSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure<CartSummaryResponse>(CartErrors.NotFound);

        var updateResult = cart
            .UpdateRentCartOffer(cartItemId, request.Quantity, request.RentalDays);

        if (updateResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(updateResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        var cartSummaryResult = await CalculateCartSummary(cart);
        if (cartSummaryResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(cartSummaryResult.Error);

        return cartSummaryResult.Value;
    }

    public async Task<Result<CartSummaryResponse>> UpdateSaleCartOfferAsync(Guid userId, Guid cartItemId, SaleCartOfferUpdateRequest request)
    {
        var spec = new UserCartAggregateAndOffersSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure<CartSummaryResponse>(CartErrors.NotFound);

        var updateResult = cart.UpdateSaleCartOffer(cartItemId, request.Quantity);
        if (updateResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(updateResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        var cartSummaryResult = await CalculateCartSummary(cart);
        if (cartSummaryResult.IsFailure)
            return Result.Failure<CartSummaryResponse>(cartSummaryResult.Error);

        return cartSummaryResult.Value;
    }

    public async Task<Result> ClearCartAsync(Guid userId)
    {
        var spec = new UserCartAggregateSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);

        if (cart is null)
            return Result.Failure(CartErrors.NotFound);

        cart.ClearCart();

        await _cartRepository.UpdateAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
