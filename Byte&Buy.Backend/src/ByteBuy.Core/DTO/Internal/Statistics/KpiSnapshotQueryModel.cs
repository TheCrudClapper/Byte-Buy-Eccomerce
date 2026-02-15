using ByteBuy.Core.Domain.ValueObjects;
namespace ByteBuy.Core.DTO.Internal.Statistics;

public sealed record KpiSnapshotQueryModel
{
    public int Users { get; init; }
    public int Employees { get; init; }
    public int Orders { get; init; }

    public Money Gmv { get; init; } = Money.Zero;
    public Money CashFlow { get; init; } = Money.Zero;

    public int ActiveSellers { get; init; }
}