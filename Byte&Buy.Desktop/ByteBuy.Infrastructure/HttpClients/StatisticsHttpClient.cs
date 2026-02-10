using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class StatisticsHttpClient : HttpClientBase, IStatisticsHttpClient
{
    private const string resource = "statistics";
    public StatisticsHttpClient(HttpClient httpClient) : base(httpClient){}

    public async Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync()
        => await GetAsync<IReadOnlyCollection<KeyPerformanceIndicatorDto>>($"{resource}/kpi");

    public async Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerTypeAsync()
         => await GetAsync<IReadOnlyCollection<GMVBySellerTypeDto>>($"{resource}/gmv-seller-type");
}
