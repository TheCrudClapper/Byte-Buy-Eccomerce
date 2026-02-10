using ByteBuy.Core.DTO.Internal.Statistics;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IStatisticsRepository
{
    Task<KpiSnapshotQuery> GetBasicKpisAsync(CancellationToken ct = default);
}
