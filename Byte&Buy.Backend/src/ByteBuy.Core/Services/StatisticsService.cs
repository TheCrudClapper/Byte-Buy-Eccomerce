using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Statistics;
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

    public async Task<Result<IReadOnlyCollection<KeyPerformanceIndicatorDto>>> GetKpisAsync()
    {
        var kpis = await _statisticsRepo.GetBasicKpisAsync();

        var response = new List<KeyPerformanceIndicatorDto>
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
                Label = "Gross Merchandise Value",
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

        return response;
    }
}
