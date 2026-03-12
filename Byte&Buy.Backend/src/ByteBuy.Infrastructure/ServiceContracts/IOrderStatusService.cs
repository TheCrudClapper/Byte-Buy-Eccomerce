namespace ByteBuy.Infrastructure.ServiceContracts;

public interface IOrderStatusService
{
    Task CancelUnpaidOrders();
}
