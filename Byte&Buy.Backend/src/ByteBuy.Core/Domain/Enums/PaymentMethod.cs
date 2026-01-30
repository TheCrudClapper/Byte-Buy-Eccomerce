using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum PaymentMethod
{
    [Description("Blik")]
    Blik = 0,

    [Description("Card")]
    Card = 1,
}
