using ByteBuy.Infrastructure.ServiceContracts;

namespace ByteBuy.Infrastructure.HangfireJobs;

public class OrderStatusJob
{
    private readonly IOrderStatusService _orderStatusService;
    public OrderStatusJob(IOrderStatusService orderStatusService)
     => _orderStatusService = orderStatusService;

    public async Task Execute()
    {
        await _orderStatusService.CancelUnpaidOrders();
    }
}
