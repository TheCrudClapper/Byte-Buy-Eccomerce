using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

//Enum that describes states order group can go throught
public enum OrderGroupStatus
{
    // Initial State of all groups
    [Description("Awaiting Payment")]
    AwaitingPayment = 0,

    // Payment started, yet not fully went throught
    [Description("Payment in progress")]
    PaymentInProgress = 1,

    // Fully paid by customer
    [Description("Paid")]
    Paid = 2,

    // Canceled before making payment
    [Description("Canceled")]
    Canceled = 3,

    // Failed - payment failed / timeout / external error
    [Description("Failed")]
    Failed = 4,
}
