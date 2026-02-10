using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Statistics;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepo;
    public StatisticsService(IStatisticsRepository statisticsRepo)
    {
        _statisticsRepo = statisticsRepo;
    }

    public async Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync(CancellationToken ct)
    {
        var kpis = await _statisticsRepo.GetBasicKpisAsync(ct);

        return new List<KeyPerformanceIndicatorDto>
        {
            new() {
                Key = KpiKeys.Users,
                Label = "Users",
                Value = kpis.Users,
                DisplayValue = kpis.Users.ToString()
            },
            new() {
                Key = KpiKeys.Employees,
                Label = "Employees",
                Value = kpis.Employees,
                DisplayValue = kpis.Employees.ToString()
            },
            new() {
                Key = KpiKeys.Orders,
                Label = "Orders",
                Value = kpis.Orders,
                DisplayValue = kpis.Orders.ToString()
            },
            new() {
                Key = KpiKeys.Gmv,
                Label = "GMV",
                Value = kpis.Gmv.Amount,
                DisplayValue = $"{kpis.Gmv.Amount:N2} {kpis.Gmv.Currency}"
            },
            new() {
                Key = KpiKeys.CashFlow,
                Label = "Cash Flow",
                Value = kpis.CashFlow.Amount,
                DisplayValue = $"{kpis.CashFlow.Amount:N2} {kpis.CashFlow.Currency}"
            },
            new() {
                Key = KpiKeys.ActiveSellers,
                Label = "Active Sellers",
                Value = kpis.ActiveSellers,
                DisplayValue = kpis.ActiveSellers.ToString()
            }
        };
    }

    public async Task<Result<IReadOnlyCollection<GMVBySellerTypeDto>>> GetGMVBySellerType(CancellationToken ct)
    {
        var gmvs = await _statisticsRepo.GetGMVBySellerTypeAsync(ct);

        return new List<GMVBySellerTypeDto>()
        {
            new()
            {
                Display = "Company",
                GMVAmount = gmvs.CompanyGMV.ToMoneyDto(),
            },
            new()
            {
                Display = "Private Sellers",
                GMVAmount = gmvs.PrivateSellerGMV.ToMoneyDto(),
            },
        };
    }

    public async Task<Result<IReadOnlyList<OrdersAndGmvByMonthDto>>> GetOrdersAndGmvByMonthAsync(int months = 6, CancellationToken ct = default)
    {
        var data = await _statisticsRepo.GetOrdersAndGmvByMonthAsync(months, ct);
        return Result.Success(data);
    }
}
