namespace ByteBuy.Core.Domain.Enums;

public enum OrderStatus
{
    Created = 0,
    AwaitingPayment,
    Paid,
    PartiallyPaid,
    Cancelled
}
