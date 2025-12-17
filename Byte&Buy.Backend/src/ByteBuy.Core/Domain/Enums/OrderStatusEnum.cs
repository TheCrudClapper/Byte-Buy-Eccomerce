using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum OrderStatusEnum
{
    [Description("Created")]
    Created = 0,

    [Description("Awaiting Payment")]
    AwaitingPayment = 1,

    [Description("Paid")]
    Paid = 2,

    [Description("Partially Paid")]
    PartiallyPaid = 3,

    [Description("Cancelled")]
    Cancelled = 4,

    [Description("Cancelled")]
    Refunded = 5,
}
