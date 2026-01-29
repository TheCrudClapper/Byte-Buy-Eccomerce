using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.Cart;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class OrderService : IOrderService
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    public OrderService(ICartRepository cartRepository, IOrderRepository orderRepository)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, OrderAddRequest request)
    {
        //var cartOffers = await _cartRepository.GetCartOffersForCheckout(userId);
        //if (cartOffers.Count == 0)
        //    return Result.Failure<CreatedResponse>(OrderErrors.NoCartOffersFound);

        //var validationResult = await ValidateOrderData(cartOffers, request);
        //if (validationResult.IsFailure)
        //    return Result.Failure<CreatedResponse>(validationResult.Error);

        //var paymentMethod = (PaymentMethod)request.PaymentMethodId;

        //var offersBySeller = cartOffers
        //    .GroupBy(o => o.SellerId)
        //    .ToDictionary(c => c.Key, c => c.ToList());

        //var orders = new List<Order>();

        //// creating order per seller
        //foreach(var sellerGroup in offersBySeller)
        //{
        //    var sellerId = sellerGroup.Key;
        //    var offers = sellerGroup.Value;

        //    var deliveryRequest = request.SelectedDeliveries
        //        .Single(d => d.SellerId == sellerId);

        //}
        //var orders = cartOffers
        //    .GroupBy(c => c.SellerId)
        //    .Select(g => Order)
        throw new NotImplementedException();
    }

    public Task<Result> ReturnOrder(Guid userId, Guid orderId)
    {
        throw new NotImplementedException();
    }

    private async Task<Result> ValidateOrderData(IReadOnlyCollection<FlatCartOffersQuery> cartOffers, OrderAddRequest request)
    {
        //if (!Enum.IsDefined(typeof(PaymentMethod), request.PaymentMethodId))
        //    return Result.Failure(OrderErrors.InvalidPaymentMethod);

        //var cartSellers = cartOffers
        //    .GroupBy(c => c.SellerId)
        //    .ToDictionary(g => g.Key, g => g.ToList());

        //var requestSellerIds = request
        //    .SelectedDeliveries
        //    .Select(s => s.SellerId)
        //    .ToHashSet();

        //if (cartSellers.Count != requestSellerIds.Count)
        //    return Result.Failure(OrderErrors.MisingDeliveryPerSeller);

        //foreach(var delivery in request.SelectedDeliveries)
        //{
        //    if (!cartSellers.TryGetValue(delivery.SellerId, out var sellerOffers))
        //        return Result.Failure(OrderErrors.InvalidSeller);

        //    var avaliableDeliveries = CheckoutService.ResolveCommonDeliveries(sellerOffers);

           

        //}

        throw new NotImplementedException();
    }
}
