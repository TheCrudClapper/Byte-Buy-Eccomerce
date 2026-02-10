using ByteBuy.Core.DTO.Internal.Statistics;
using ByteBuy.Core.DTO.Public.Statistics;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IStatisticsRepository
{
    Task<KpiSnapshotQuery> GetBasicKpisAsync(CancellationToken ct = default);
    Task<GMVBySellerTypeQuery> GetGMVBySellerTypeAsync(CancellationToken ct = default);
    Task<IReadOnlyList<OrdersAndGmvByMonthDto>> GetOrdersAndGmvByMonthAsync(int months = 6, CancellationToken ct = default);
}
