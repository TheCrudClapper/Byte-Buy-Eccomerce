using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class OrderService(ICompanyOrderHttpClient httpClient) : IOrderService
{
    public Task<Result> DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<UpdatedResponse>> DeliverOrder(Guid orderId)
        => await httpClient.DeliverOrderAsync(orderId);

    public async Task<Result<PagedList<CompanyOrderListResponse>>> GetCompanyOrderList(OrderListQuery query, CancellationToken ct = default)
        => await httpClient.GetCompanyOrdersListAsync(query);

    public async Task<Result<OrderDetailsResponse>> GetOrderDetails(Guid orderId)
        => await httpClient.GetCompanyOrderDetailsAsync(orderId);

    public async Task<Result<UpdatedResponse>> ShipOrder(Guid orderId)
        => await httpClient.ShipOrderAsync(orderId);

    public async Task<Result<IReadOnlyCollection<OrderDashboardListResponse>>> GetDashboardOrders()
        => await httpClient.GetDashboardListAsync();
}
