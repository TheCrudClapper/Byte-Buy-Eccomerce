using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IOrderService
{
    Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetUserOrdersAsync(Guid userId, CancellationToken ct = default);

    Task<Result<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId, CancellationToken ct = default);
    Task<Result<OrderDetailsResponse>> GetOrderDetailsAsync(Guid userId, Guid orderId, CancellationToken ct = default);

    Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetSellerOrdersAsync(Guid sellerId, CancellationToken ct = default);

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

    /// <summary>
    /// Method that is designated only for private sellers, allows sending offer to clients
    /// </summary>
    /// <param name="sellerId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> ShipOrderAsPrivateSeller(Guid sellerId, Guid orderId);

    /// <summary>
    /// Method designated only for company, allows sending offers to clients
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> ShipOrderAsCompany(Guid orderId);

    /// <summary>
    /// Method that is designated only for seller, allows delivering orders
    /// </summary>
    /// <param name="sellerId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> DeliverOrderAsPrivateSeller(Guid sellerId, Guid orderId);

    /// <summary>
    /// Method that is designated only for company, allows delivering orders.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> DeliverOrderAsCompany(Guid orderId);

    /// <summary>
    /// Method that is designated only for company context
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrdersListAsync(CancellationToken ct = default);
}
