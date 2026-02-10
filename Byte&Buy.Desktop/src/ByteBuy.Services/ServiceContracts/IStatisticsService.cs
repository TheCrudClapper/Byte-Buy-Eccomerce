using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IStatisticsService
{
    Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpis();
    Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGmvBySellerType();
    Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersGmvByMonths();
}
