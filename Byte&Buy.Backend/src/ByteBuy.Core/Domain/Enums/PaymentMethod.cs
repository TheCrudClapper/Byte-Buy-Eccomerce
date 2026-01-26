using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum PaymentMethod
{
    [Description("Blik")]
    Blik,

    [Description("Card")]
    Card,

    [Description("Google Pay")]
    GooglePay,
}
