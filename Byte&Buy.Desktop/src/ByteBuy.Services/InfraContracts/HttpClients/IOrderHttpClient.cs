using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IOrderHttpClient
{
    Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrdersListAsync();
    Task<Result<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId);
    Task<Result<UpdatedResponse>> DeliverOrderAsync(Guid orderId);
    Task<Result<UpdatedResponse>> ShipOrderAsync(Guid orderId);
    Task<Result<IReadOnlyCollection<OrderDashboardListResponse>>> GetDashboardListAsync();
}
