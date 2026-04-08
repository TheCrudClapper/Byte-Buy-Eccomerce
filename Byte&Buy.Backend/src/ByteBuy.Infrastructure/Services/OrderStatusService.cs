using ByteBuy.Core.Domain.Orders.Enums;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Services;

public class OrderStatusService : IOrderStatusService
{
    private readonly ApplicationDbContext _context;
    public OrderStatusService(ApplicationDbContext context)
       => _context = context;

    public async Task CancelUnpaidOrders()
    {
        var thresholdDate = DateTime.UtcNow.AddHours(-24);

        await using var transaction = await _context.Database.BeginTransactionAsync();

        var orders = await _context.Orders
            .Where(o => o.Status == OrderStatus.AwaitingPayment && o.DateCreated <= thresholdDate)
            .ToListAsync();

        List<Guid> offerIds = [];

        try
        {
            //Cancel orders
            foreach (var order in orders)
            {
                order.ChangeStatus(OrderStatus.SystemCanceled);
                offerIds.AddRange(order.Lines.Select(line => line.OfferId));
            }

            //Restore offer quantity
            var offers = await _context.Offers
                .Where(o => offerIds.Contains(o.Id))
                .ToListAsync();

            var offerLookup = offers.ToDictionary(o => o.Id);

            foreach (var order in orders)
            {
                foreach (var line in order.Lines)
                {
                    if (offerLookup.TryGetValue(line.OfferId, out var offer))
                    {
                        offer.RestoreQuantity(line.Quantity);
                    }
                }
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
        }
    }
}
