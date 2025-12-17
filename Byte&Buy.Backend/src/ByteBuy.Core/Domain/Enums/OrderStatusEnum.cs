namespace ByteBuy.Core.Domain.Enums;

public enum OrderStatusEnum
{
    Created = 0,
    AwaitingPayment,
    Paid,
    PartiallyPaid,
    Cancelled
}
