using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IOrderService
{
    Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetAllUserOrders(Guid userId, CancellationToken ct = default);
    Task<Result<OrderDetailsResponse>> GetOrderDetails(Guid userId, Guid orderId, CancellationToken ct = default);

    /// <summary>
    /// Method that cancels user's order, can only be invoked by owner of offer.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> CancelOrder(Guid userId, Guid orderId);

    /// <summary>
    /// Method that allows user to return order up to 14 days time after purchase
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> ReturnOrder(Guid userId, Guid orderId);
}
