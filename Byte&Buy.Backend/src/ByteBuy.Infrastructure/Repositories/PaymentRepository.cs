using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class PaymentRepository : EfBaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext context) : base(context){ }

    public async Task<Payment?> GetPaymentByUserId(Guid userId, Guid paymentId, CancellationToken ct)
    {
        return await _context.PaymentOrders
             .Where(p => p.PaymentId == paymentId && p.Order.BuyerId == userId)
             .Select(p => p.Payment)
             .FirstOrDefaultAsync(ct);
    }
}
