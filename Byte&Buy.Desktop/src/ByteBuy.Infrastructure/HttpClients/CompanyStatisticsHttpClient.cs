using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyStatisticsHttpClient : HttpClientBase, IStatisticsHttpClient
{
    private const string resource = "company/statistics";
    public CompanyStatisticsHttpClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync()
        => await GetAsync<IReadOnlyCollection<KeyPerformanceIndicatorDto>>($"{resource}/kpi");

    public async Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerTypeAsync()
         => await GetAsync<IReadOnlyCollection<GMVBySellerTypeDto>>($"{resource}/gmv-seller-type");

    public async Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonths()
        => await GetAsync<IReadOnlyList<OrdersAndGmvByMonthDto>>($"{resource}/gmv-months");
}
