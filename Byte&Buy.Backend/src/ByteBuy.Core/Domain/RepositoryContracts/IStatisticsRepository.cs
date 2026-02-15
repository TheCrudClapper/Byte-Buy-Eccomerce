using ByteBuy.Core.DTO.Internal.Statistics;
using ByteBuy.Core.DTO.Public.Statistics;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IStatisticsRepository
{
    /// <summary>
    /// Gets the basic Key Performace Indicators used in system
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<KpiSnapshotQueryModel> GetBasicKpisAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets the Gross Merchandise Value split into company and private seller gmv
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<GMVBySellerTypeQueryModel> GetGMVBySellerTypeAsync(CancellationToken ct = default);

    /// <summary>
    /// Get order count per given month count and gmv by month
    /// </summary>
    /// <param name="months"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<IReadOnlyList<OrdersAndGmvByMonthDto>> GetOrdersAndGmvByMonthAsync(int months = 6, CancellationToken ct = default);
}
