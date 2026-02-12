using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IOrderService : IBaseService
{
    Task<Result<PagedList<CompanyOrderListResponse>>> GetCompanyOrderList(CancellationToken ct = default);
    Task<Result<OrderDetailsResponse>> GetOrderDetails(Guid orderId);
    Task<Result<IReadOnlyCollection<OrderDashboardListResponse>>> GetDashboardOrders();
    Task<Result<UpdatedResponse>> DeliverOrder(Guid orderId);
    Task<Result<UpdatedResponse>> ShipOrder(Guid orderId);
}
