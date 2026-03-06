using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Configuration;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyOrdersHttpClient(HttpClient httpClient, IConfiguration config)
    : HttpClientBase(httpClient, config), IOrderHttpClient
{
    private const string resource = "company/orders";

    public Task<Result<PagedList<CompanyOrderListResponse>>> GetCompanyOrdersListAsync(OrderListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}", query);
        return GetAsync<PagedList<CompanyOrderListResponse>>(url);
    }

    public async Task<Result<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId)
        => await GetAsync<OrderDetailsResponse>($"{resource}/details/{orderId}");

    public async Task<Result<UpdatedResponse>> DeliverOrderAsync(Guid orderId)
        => await PutAsync<UpdatedResponse>($"{resource}/{orderId}/deliver", null);

    public async Task<Result<UpdatedResponse>> ShipOrderAsync(Guid orderId)
        => await PutAsync<UpdatedResponse>($"{resource}/{orderId}/ship/", null);

    public async Task<Result<IReadOnlyCollection<OrderDashboardListResponse>>> GetDashboardListAsync()
        => await GetAsync<IReadOnlyCollection<OrderDashboardListResponse>>($"{resource}/dashboard");
}
