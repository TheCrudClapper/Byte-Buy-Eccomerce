using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class OrderRepository : EfBaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context){ }

    public async Task<IReadOnlyCollection<Order>> GetOrdersByPaymentId(Guid userId, Guid paymentId)
    {
        return await _context.PaymentOrders
            .Where(po => po.PaymentId == paymentId && po.Order.BuyerId == userId)
            .Select(o => o.Order)
            .ToListAsync();
    }
}
