using ByteBuy.Services.DTO.Statistics;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyStatisticsHttpClient
{
    Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync();
    Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerTypeAsync();
    Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonthsAsync();
}
