using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IOrderService : IBaseService
{
    Task<Result<PagedList<CompanyOrderListResponse>>> GetCompanyOrderListAsync(OrderListQuery query, CancellationToken ct = default);
    Task<Result<OrderDetailsResponse>> GetOrderDetailsAsync(Guid orderId);
    Task<Result<IReadOnlyCollection<OrderDashboardListResponse>>> GetDashboardOrdersAsync();
    Task<Result<UpdatedResponse>> DeliverOrderAsync(Guid orderId);
    Task<Result<UpdatedResponse>> ShipOrderAsync(Guid orderId);
}
