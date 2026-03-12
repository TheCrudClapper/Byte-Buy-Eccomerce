using ByteBuy.Core.Domain.Orders.Enums;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Services;

public class OrderStatusService : IOrderStatusService
{
    private readonly ApplicationDbContext _context;
    public OrderStatusService(ApplicationDbContext context)
       =>  _context = context;
    
    public async Task CancelUnpaidOrders()
    {
        var thresholdDate = DateTime.UtcNow.AddHours(-24);

        var orders = await _context.Orders
            .Where(o => o.Status == OrderStatus.AwaitingPayment && o.DateCreated <= thresholdDate)
            .ToListAsync();

        foreach(var order in orders)
            order.ChangeStatus(OrderStatus.SystemCanceled);
        
        await _context.SaveChangesAsync();
    }
}
