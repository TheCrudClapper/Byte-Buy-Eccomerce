using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Internal.Statistics;
using ByteBuy.Core.DTO.Public.Statistics;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly ApplicationDbContext _context;

    public StatisticsRepository(ApplicationDbContext context)
        => _context = context;


    public async Task<KpiSnapshotQueryModel> GetBasicKpisAsync(CancellationToken ct = default)
    {
        var users = await _context.PortalUsers.CountAsync(ct);
        var employees = await _context.Employees.CountAsync(ct);

        // counting all orders except canceled
        var orders = await _context.Orders.Where(o => o.Status != OrderStatus.Canceled)
            .CountAsync(ct);

        // gmv calculated based on ONLY 
        var gmv = await _context.Orders
            .Where(o => o.Status != OrderStatus.Canceled)
            .SumAsync(o => (decimal?)o.LinesTotal.Amount, ct) ?? 0m;

        // summing all paid, completed payments
        var cashFlow = await _context.Payments
            .Where(o => o.Status == PaymentStatus.Completed)
            .SumAsync(p => (decimal?)p.Amount.Amount, ct) ?? 0m;
        
        // counting all private seller that sold anything
        var activeSellers = await _context.Orders
            .Where(o => o.SellerSnapshot.Type == SellerType.PrivatePerson)
            .Select(o => o.SellerSnapshot.SellerId)
            .Distinct()
            .CountAsync(ct);

        return new KpiSnapshotQueryModel
        {
            Users = users,
            Employees = employees,
            Orders = orders,
            Gmv = gmv == 0 ? Money.Zero : Money.Create(gmv).Value,
            CashFlow = cashFlow == 0 ? Money.Zero : Money.Create(cashFlow).Value,
            ActiveSellers = activeSellers
        };
    }

    public async Task<GMVBySellerTypeQueryModel> GetGMVBySellerTypeAsync(CancellationToken ct = default)
    {
        // calulcation of private sellers gmv
        var privateSellerGMV = await _context.Orders
            .Where(o => o.Status != OrderStatus.Canceled && o.SellerSnapshot.Type == SellerType.PrivatePerson)
            .SumAsync(o => (decimal?)o.LinesTotal.Amount, ct) ?? 0m;

        // calculation of company's gmv
        var companyGMV = await _context.Orders
            .Where(o => o.Status != OrderStatus.Canceled && o.SellerSnapshot.Type == SellerType.Company)
            .SumAsync(o => (decimal?)o.LinesTotal.Amount, ct) ?? 0m;

        var response = new GMVBySellerTypeQueryModel
        {
            CompanyGMV = companyGMV == 0 ? Money.Zero : Money.Create(companyGMV).Value,
            PrivateSellerGMV = privateSellerGMV == 0 ? Money.Zero : Money.Create(privateSellerGMV).Value
        };

        return response;
    }

    public async Task<IReadOnlyList<OrdersAndGmvByMonthDto>> GetOrdersAndGmvByMonthAsync(int months = 6, CancellationToken ct = default)
    {
        // last x months with current one
        var startDate = DateTime.UtcNow.AddMonths(-months + 1);

        var orders = await _context.Orders
           .Where(o => o.Status != OrderStatus.Canceled && o.DateCreated >= startDate)
           .Select(o => new
           {
               o.DateCreated,
               o.LinesTotal.Amount
           })
           .ToListAsync(ct);

        var grouped = orders
            .GroupBy(o => new { o.DateCreated.Year, o.DateCreated.Month })
            .Select(g => new OrdersAndGmvByMonthDto(
                g.Key.Year,
                g.Key.Month,
                g.Count(),
                g.Sum(x => x.Amount)
            ))
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToList();

        var response = new List<OrdersAndGmvByMonthDto>();
        for (int i = 0; i < months; i++)
        {
            var dt = startDate.AddMonths(i);
            var date = grouped.FirstOrDefault(x => x.Year == dt.Year && x.Month == dt.Month);
            response.Add(date ?? new OrdersAndGmvByMonthDto(dt.Year, dt.Month, 0, 0m));
        }

        return response;
    }
}
