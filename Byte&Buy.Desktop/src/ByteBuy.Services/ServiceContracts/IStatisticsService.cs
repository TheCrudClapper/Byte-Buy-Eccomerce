using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IStatisticsService
{
    Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync();
    Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGmvBySellerTypeAsync();
    Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersGmvByMonthsAsync();
}
