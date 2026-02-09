using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum OfferStatus
{
    [Description("Available")]
    Available = 0,

    [Description("Sold Out")]
    SoldOut = 1
}
