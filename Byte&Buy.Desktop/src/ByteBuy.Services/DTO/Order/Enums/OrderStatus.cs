namespace ByteBuy.Services.DTO.Order.Enums;

public enum OrderStatus
{
    AwaitingPayment = 0,
    Paid = 1,
    Shipped = 2,
    Delivered = 3,
    Canceled = 4,
    Returned = 5
}
