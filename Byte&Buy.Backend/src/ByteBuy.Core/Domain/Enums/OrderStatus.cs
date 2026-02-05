using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum OrderStatus
{
    /// <summary>
    /// Status in which order is created
    /// </summary>
    [Description("Awaiting Payment")]
    AwaitingPayment = 0,

    [Description("Paid")]
    Paid = 1,

    /// <summary>
    /// Status when order is being shipped to the client
    /// </summary>
    [Description("Shipped")]
    Shipped = 2,

    /// <summary>
    /// Status when order and its items were delivered to client
    /// </summary>
    [Description("Delivered")]
    Delivered = 3,

    /// <summary>
    /// Staus when order is canceled BEFORE payment
    /// </summary>
    [Description("Canceled")]
    Canceled = 4,

    /// <summary>
    /// Status when orders is returned within first 14 days
    /// </summary>
    [Description("Returned")]
    Returned = 5
}
