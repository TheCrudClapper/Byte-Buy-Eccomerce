using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum OrderStatus
{
    [Description("Awaiting Payment")]
    AwaitingPayment = 0,

    [Description("Paid")]
    Paid = 1,

    [Description("Shipped")]
    Shipped = 2,

    [Description("Delivered")]
    Delivered = 3,

    [Description("Canceled")]
    Canceled = 4,

    [Description("Returned")]
    Returned = 5
}
