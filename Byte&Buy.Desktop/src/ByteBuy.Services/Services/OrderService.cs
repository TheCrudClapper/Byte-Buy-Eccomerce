using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class OrderService(IOrderHttpClient httpClient) : IOrderService
{
    public Task<Result> DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<UpdatedResponse>> DeliverOrder(Guid orderId)
        => await httpClient.DeliverOrderAsync(orderId);

    public async Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrderList(CancellationToken ct = default)
        => await httpClient.GetCompanyOrdersListAsync();

    public async Task<Result<OrderDetailsResponse>> GetOrderDetails(Guid orderId)
        => await httpClient.GetCompanyOrderDetailsAsync(orderId);

    public async Task<Result<UpdatedResponse>> ShipOrder(Guid orderId)
        => await httpClient.ShipOrderAsync(orderId);
}
