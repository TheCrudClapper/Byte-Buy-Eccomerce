using ByteBuy.Core.Domain.DeliveryCarriers;
using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.DTO.Public.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class DeliveryCarrierMappings
{
    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this DeliveryCarrier carrier)
            => new SelectListItemResponse<Guid>(carrier.Id, carrier.Name);

    public static DeliveryCarrierResponse ToDeliveryCarrierResponse(this DeliveryCarrier carrier)
        => new DeliveryCarrierResponse(carrier.Id, carrier.Name, carrier.Code);

    public static Expression<Func<DeliveryCarrier, DeliveryCarrierResponse>> DeliveryCarrierResponseProjection
        => d => new DeliveryCarrierResponse(d.Id, d.Name, d.Code);
}
