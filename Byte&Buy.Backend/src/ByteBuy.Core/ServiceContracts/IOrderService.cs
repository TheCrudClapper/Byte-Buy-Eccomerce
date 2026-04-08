using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.ServiceContracts;

public interface IOrderService
{
    Task<Result<PagedList<UserOrderListResponse>>> GetUserOrdersAsync(UserOrderListQuery queryParams, Guid userId, CancellationToken ct = default);
    Task<Result<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId, CancellationToken ct = default);
    Task<Result<OrderDetailsResponse>> GetOrderDetailsAsync(Guid userId, Guid orderId, CancellationToken ct = default);
    Task<Result<PagedList<UserOrderListResponse>>> GetSellerOrdersAsync(UserOrderSellerListQuery queryParams, Guid sellerId, CancellationToken ct = default);

    /// <summary>
    /// Method that cancels user's order, can only be invoked by owner of offer.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> CancelOrderAsync(Guid userId, Guid orderId);

    /// <summary>
    /// Method that allows user to return order up to 14 days time after purchase
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> ReturnOrderAsync(Guid userId, Guid orderId);

    /// <summary>
    /// Method that is designated only for private sellers, allows sending offer to clients
    /// </summary>
    /// <param name="sellerId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> ShipOrderAsPrivateSellerAsync(Guid sellerId, Guid orderId);

    /// <summary>
    /// Method designated only for company, allows sending offers to clients
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> ShipOrderAsCompanyAsync(Guid orderId);

    /// <summary>
    /// Method that is designated only for seller, allows delivering orders
    /// </summary>
    /// <param name="sellerId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> DeliverOrderAsPrivateSellerAsync(Guid sellerId, Guid orderId);

    /// <summary>
    /// Method that is designated only for company, allows delivering orders.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result<UpdatedResponse>> DeliverOrderAsCompanyAsync(Guid orderId);

    /// <summary>
    /// Method that is designated only for company context
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<Result<PagedList<CompanyOrderListResponse>>> GetCompanyOrdersListAsync(OrderCompanyListQuery queryParams, CancellationToken ct = default);

    /// <summary>
    /// Method that takes recent orders
    /// </summary>
    /// <returns></returns>
    Task<Result<IReadOnlyCollection<OrderDashboardListResponse>>> GetDashboardOrdersAsync(CancellationToken ct = default);

    /// <summary>
    /// Method that deactivate order when is not longer needed to be displayed
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<Result> DeleteAsync(Guid userId, Guid orderId);

}
