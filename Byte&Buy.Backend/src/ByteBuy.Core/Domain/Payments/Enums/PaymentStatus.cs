using System.ComponentModel;

namespace ByteBuy.Core.Domain.Payments.Enums;

public enum PaymentStatus
{
    /// <summary>
    /// Status that describes unpaid payment
    /// </summary>
    [Description("Created")]
    Created = 0,

    /// <summary>
    /// Status that describes completed - PAID payment
    /// </summary>
    [Description("Completed")]
    Completed = 2,
}
