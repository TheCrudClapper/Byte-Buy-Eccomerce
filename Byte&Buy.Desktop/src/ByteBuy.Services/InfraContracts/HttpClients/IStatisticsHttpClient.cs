using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;
public interface IStatisticsHttpClient
{
    Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync();
    Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerTypeAsync();
}
