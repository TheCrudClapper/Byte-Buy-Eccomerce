using ByteBuy.Core.DTO.Public.Statistics;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IStatisticsService
{
    Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync(CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerType(CancellationToken ct = default);
    Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonthAsync(int months = 6, CancellationToken ct = default);
}
