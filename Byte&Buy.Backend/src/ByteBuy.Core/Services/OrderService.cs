using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.OrderSpecifications;

namespace ByteBuy.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<UpdatedResponse>> CancelOrder(Guid userId, Guid orderId)
    {
        var order = await _orderRepository.GetUserOrder(userId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var cancelationResult =  order.CancelOrder();
        if(cancelationResult.IsFailure)
            return Result.Failure<UpdatedResponse>(cancelationResult.Error);

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.CommitAsync();

        return order.ToUpdatedResponse();
    }


    public async Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetAllUserOrders(Guid userId, CancellationToken ct = default)
    {
        var spec = new UserOrderListQuerySpec(userId);
        var queryResult = await _orderRepository.GetListBySpecAsync(spec, ct);

        return queryResult
            .Select(o => o.ToUserOrderListResponse())
            .ToList();
    }

    public async Task<Result<OrderDetailsResponse>> GetOrderDetails(Guid userId, Guid orderId, CancellationToken ct = default)
    {
        var spec = new OrderDetailsResponseSpec(userId, orderId);
        var queryResult = await _orderRepository.GetBySpecAsync(spec, ct);

        return queryResult is null
            ? Result.Failure<OrderDetailsResponse>(OrderErrors.NotFound)
            : queryResult.ToOrderDetailResponse();
    }

    public async Task<Result<UpdatedResponse>> ReturnOrder(Guid userId, Guid orderId)
    {
        var order = await _orderRepository.GetUserOrder(userId, orderId);
        if (order is null)
            return Result.Failure<UpdatedResponse>(OrderErrors.NotFound);

        var returnResult = order.ReturnOrder();
        if (returnResult.IsFailure)
            return Result.Failure<UpdatedResponse>(returnResult.Error);

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.CommitAsync();

        return order.ToUpdatedResponse();
    }
}
