using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum OrderStatus
{
    [Description("Created")]
    Created = 0,

    [Description("In Preparation")]
    InPreparation = 1,

    [Description("Shipped")]
    Shipped = 2,

    [Description("Delivered")]
    Delivered = 3,

    [Description("Completed")]
    Completed = 4,

    [Description("Canceled")]
    Canceled = 5,

    [Description("Returned")]
    Returned = 6
}
