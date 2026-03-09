using System.ComponentModel;

namespace ByteBuy.Core.Domain.Deliveries.Enums;

public enum ParcelSize
{
    [Description("Small")]
    Small = 0,

    [Description("Medium")]
    Medium = 1,

    [Description("Large")]
    Large = 2
}
