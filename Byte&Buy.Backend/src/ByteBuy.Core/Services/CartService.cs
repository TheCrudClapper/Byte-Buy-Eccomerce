using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Cart;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.CartSpecifications;

namespace ByteBuy.Core.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IOfferRepository _offerRepository;
    public CartService(ICartRepository cartRepository, IOfferRepository offerRepository)
    {
        _cartRepository = cartRepository;
        _offerRepository = offerRepository;
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
            return Result.Failure(CartErrors.CannotAddSelfOfferToCart);

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
            return Result.Failure(CartErrors.CannotAddSelfOfferToCart);

        var result = cart.AddSaleCartOffer(saleOffer, request.Quantity);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteCartOffer(Guid userId, Guid cartItemId)
    {
        var spec = new CartAggregateWithOffersByUserIdSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure(Error.NotFound);

        var deleteResult = cart.RemoveCartItem(cartItemId);
        if (deleteResult.IsFailure)
            return Result.Failure(deleteResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateRentCartOffer(Guid userId, Guid cartItemId, RentCartOfferUpdateRequest request)
    {
        var spec = new CartAggregateWithOffersByUserIdSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure(Error.NotFound);

        var cartOffer = cart.CartOffers
            .FirstOrDefault(co => co.Id == cartItemId);

        if (cartOffer is null || cartOffer is not RentCartOffer)
            return Result.Failure(Error.NotFound);

        var offer = await _offerRepository.GetByIdAsync(cartOffer.OfferId);
        if (offer is not RentOffer rentOffer)
            return Result.Failure(Error.NotFound);

        var updateResult = cart
            .UpdateRentCartOffer(rentOffer, cartItemId, request.Quantity, request.RentalDays);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();
        return Result.Success();
    }

    public async Task<Result> UpdateSaleCartOffer(Guid userId, Guid cartItemId, SaleCartOfferUpdateRequest request)
    {
        var spec = new CartAggregateWithOffersByUserIdSpec(userId);
        var cart = await _cartRepository.GetBySpecAsync(spec);
        if (cart is null)
            return Result.Failure(Error.NotFound);

        var cartOffer = cart.CartOffers
            .FirstOrDefault(co => co.Id == cartItemId);

        if (cartOffer is null || cartOffer is not SaleCartOffer)
            return Result.Failure(Error.NotFound);

        var offer = await _offerRepository.GetByIdAsync(cartOffer.OfferId);
        if (offer is not SaleOffer saleOffer)
            return Result.Failure(Error.NotFound);

        var updateResult = cart.UpdateSaleCartOffer(saleOffer, cartItemId, request.Quantity);
        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.CommitAsync();

        return Result.Success();
    }
}
