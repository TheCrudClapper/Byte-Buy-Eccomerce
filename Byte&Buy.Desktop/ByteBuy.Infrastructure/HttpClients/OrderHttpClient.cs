using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class OrderHttpClient(HttpClient httpClient)
    : HttpClientBase(httpClient), IOrderHttpClient
{
    private const string resource = "orders";

    public async Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrdersListAsync()
        => await GetAsync<IReadOnlyCollection<CompanyOrderListResponse>>($"{resource}/company");

    public async Task<Result<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId)
        => await GetAsync<OrderDetailsResponse>($"{resource}/details/{orderId}/company");

    public async Task<Result<UpdatedResponse>> DeliverOrderAsync(Guid orderId)
        => await PutAsync<UpdatedResponse>($"{resource}/{orderId}/deliver/company", null);

    public async Task<Result<UpdatedResponse>> ShipOrderAsync(Guid orderId)
        => await PutAsync<UpdatedResponse>($"{resource}/{orderId}/ship/company", null);

    public async Task<Result<IReadOnlyCollection<OrderDashboardListResponse>>> GetDashboardListAsync()
        => await GetAsync<IReadOnlyCollection<OrderDashboardListResponse>>($"{resource}/dashboard");
}
