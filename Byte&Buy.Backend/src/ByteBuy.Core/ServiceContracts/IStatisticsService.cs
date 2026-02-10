using ByteBuy.Core.DTO.Public.Statistics;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IStatisticsService
{
    Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync();
}
