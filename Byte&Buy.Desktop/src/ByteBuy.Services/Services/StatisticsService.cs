using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class StatisticsService(IStatisticsHttpClient httpClient) : IStatisticsService
{
    public async Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpis()
        => await httpClient.GetKpisAsync();

    public async Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGmvBySellerType()
        => await httpClient.GetGMVBySellerTypeAsync();
}
