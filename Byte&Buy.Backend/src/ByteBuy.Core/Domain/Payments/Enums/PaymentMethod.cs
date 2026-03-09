using System.ComponentModel;

namespace ByteBuy.Core.Domain.Payments.Enums;

public enum PaymentMethod
{
    [Description("Blik")]
    Blik = 0,

    [Description("Card")]
    Card = 1,
}
