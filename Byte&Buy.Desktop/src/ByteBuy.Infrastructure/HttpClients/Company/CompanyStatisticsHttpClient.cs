using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyStatisticsHttpClient(IBackendHttpClientFactory factory, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(factory), ICompanyStatisticsHttpClient
{
    private readonly string resource = options.Value.CompanyStatistics;

    public async Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync()
        => await GetAsync<IReadOnlyCollection<KeyPerformanceIndicatorDto>>($"{resource}/kpi");

    public async Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerTypeAsync()
         => await GetAsync<IReadOnlyCollection<GMVBySellerTypeDto>>($"{resource}/gmv-seller-type");

    public async Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonthsAsync()
        => await GetAsync<IReadOnlyList<OrdersAndGmvByMonthDto>>($"{resource}/gmv-months");
}
