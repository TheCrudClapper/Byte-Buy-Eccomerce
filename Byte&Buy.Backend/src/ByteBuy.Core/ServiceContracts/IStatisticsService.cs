using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Statistics;

namespace ByteBuy.Core.ServiceContracts;

public interface IStatisticsService
{
    Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync(CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerTypeAsync(CancellationToken ct = default);
    Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonthAsync(int months = 6, CancellationToken ct = default);
}
