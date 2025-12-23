using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBuy.Core.ResultTypes;

public static class OfferErrors
{
    public static readonly Error DeliveryRequired
        = new Error(400, "At least one delivery is required");
}
