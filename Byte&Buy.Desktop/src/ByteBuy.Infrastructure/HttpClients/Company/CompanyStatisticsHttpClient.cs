using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyStatisticsHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), IStatisticsHttpClient
{
    private readonly string resource = options.Value.CompanyStatistics;

    public async Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync()
        => await GetAsync<IReadOnlyCollection<KeyPerformanceIndicatorDto>>($"{resource}/kpi");

    public async Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerTypeAsync()
         => await GetAsync<IReadOnlyCollection<GMVBySellerTypeDto>>($"{resource}/gmv-seller-type");

    public async Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonths()
        => await GetAsync<IReadOnlyList<OrdersAndGmvByMonthDto>>($"{resource}/gmv-months");
}
