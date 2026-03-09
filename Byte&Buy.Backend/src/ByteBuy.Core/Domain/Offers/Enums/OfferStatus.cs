using System.ComponentModel;

namespace ByteBuy.Core.Domain.Offers.Enums;

public enum OfferStatus
{
    [Description("Available")]
    Available = 0,

    [Description("Sold Out")]
    SoldOut = 1
}
