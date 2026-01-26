using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum PaymentStatus
{
    [Description("Created")]
    Created = 0,

    [Description("Pending")]
    Pending = 1,

    [Description("Completed")]
    Completed = 2,

    [Description("Failed")]
    Failed = 3,

    [Description("Cancelled")]
    Cancelled = 4
}
