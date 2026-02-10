using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Internal.Statistics;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly ApplicationDbContext _context;

    public StatisticsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<KpiSnapshotQuery> GetBasicKpisAsync(CancellationToken ct = default)
    {
        var users = await _context.PortalUsers.CountAsync(ct);
        var employees = await _context.Employees.CountAsync(ct);

        // counting only not canceled orders
        var orders = await _context.Orders.Where(o => o.Status != OrderStatus.Canceled)
            .CountAsync(ct);

        var gmv = await _context.Orders
            .Where(o => o.Status != OrderStatus.Canceled)
            .SumAsync(o => (decimal?)o.LinesTotal.Amount, ct) ?? 0m;

        var cashFlow = await _context.Payments
            .Where(o => o.Status == PaymentStatus.Completed)
            .SumAsync(p => (decimal?)p.Amount.Amount, ct) ?? 0m;


        var activeSellers = await _context.Orders
            .Where(o => o.SellerSnapshot.Type == SellerType.PrivatePerson)
            .Select(o => o.SellerSnapshot.SellerId)
            .Distinct()
            .CountAsync(ct);

        return new KpiSnapshotQuery
        {
            Users = users,
            Employees = employees,
            Orders = orders,
            Gmv = gmv == 0 ? Money.Zero : Money.Create(gmv).Value,
            CashFlow = cashFlow == 0 ? Money.Zero : Money.Create(cashFlow).Value,
            ActiveSellers = activeSellers
        };
    }

    public async Task<GMVBySellerTypeQuery> GetGMVBySellerTypeAsync(CancellationToken ct = default)
    {
        var privateSellerGMV = await _context.Orders
            .Where(o => o.Status != OrderStatus.Canceled && o.SellerSnapshot.Type == SellerType.PrivatePerson)
            .SumAsync(o => (decimal?)o.LinesTotal.Amount, ct) ?? 0m;

        var companyGMV = await _context.Orders
            .Where(o => o.Status != OrderStatus.Canceled && o.SellerSnapshot.Type == SellerType.Company)
            .SumAsync(o => (decimal?)o.LinesTotal.Amount, ct) ?? 0m;

        var response = new GMVBySellerTypeQuery
        {
            CompanyGMV = companyGMV == 0 ? Money.Zero : Money.Create(companyGMV).Value,
            PrivateSellerGMV = privateSellerGMV == 0 ? Money.Zero : Money.Create(privateSellerGMV).Value
        };

        return response;
    }
}
