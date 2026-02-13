using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class OrderRepository : EfBaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyCollection<Order>> GetOrdersByPaymentId(Guid userId, Guid paymentId, CancellationToken ct = default)
    {
        return await _context.PaymentOrders
            .Where(po => po.PaymentId == paymentId && po.Order.BuyerId == userId)
            .Select(o => o.Order)
            .ToListAsync(ct);
    }

    public async Task<Order?> GetUserOrder(Guid userId, Guid orderId, CancellationToken ct = default)
    {
        return await _context.Orders
            .Include(o => o.Lines)
            .Where(o => o.BuyerId == userId && o.Id == orderId)
            .FirstOrDefaultAsync(ct);
    }


    public async Task<Order?> GetSellerOrder(Guid sellerId, Guid orderId, CancellationToken ct = default)
    {
        return await _context.Orders
            .Include(o => o.Lines)
            .Where(o => o.Id == orderId && o.SellerSnapshot.SellerId == sellerId)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedList<CompanyOrderListResponse>> GetOrdersList(
        OrderCompanyListQuery queryParams, Guid companyId, CancellationToken ct = default)
    {
        var query = _context.Orders
            .AsNoTracking()
            .Where(o => o.SellerSnapshot.SellerId == companyId && o.SellerSnapshot.Type == SellerType.Company);

        if (queryParams.PurchasedFrom.HasValue)
        {
            var fromUtc = DateTime.SpecifyKind(queryParams.PurchasedFrom.Value, DateTimeKind.Utc);
            query = query.Where(o => o.DateCreated >= fromUtc);
        }

        if (queryParams.PurchasedTo.HasValue)
        {
            var fromUtc = DateTime.SpecifyKind(queryParams.PurchasedTo.Value, DateTimeKind.Utc);
            query = query.Where(o => o.DateCreated <= fromUtc);
        }

        if (queryParams.TotalFrom.HasValue)
            query = query.Where(o => o.Total.Amount >= queryParams.TotalFrom.Value);

        if (queryParams.TotalTo.HasValue)
            query = query.Where(o => o.Total.Amount <= queryParams.TotalTo.Value);

        if (!string.IsNullOrWhiteSpace(queryParams.BuyerFullName))
            query = query.Where(o => o.BuyerSnapshot.FullName.Contains(queryParams.BuyerFullName));

        if (!string.IsNullOrWhiteSpace(queryParams.BuyerEmail))
            query = query.Where(o => o.BuyerSnapshot.Email.Contains(queryParams.BuyerEmail));

        var projection = query.Select(OrderMappings.CompanyOrderListProjection);

        return await projection
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }
}
