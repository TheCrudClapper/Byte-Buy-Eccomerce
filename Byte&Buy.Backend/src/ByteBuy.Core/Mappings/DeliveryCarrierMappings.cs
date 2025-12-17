using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.DeliveryCarrier;

namespace ByteBuy.Core.Mappings;

public static class DeliveryCarrierMappings
{
    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this DeliveryCarrier carrier)
            => new SelectListItemResponse<Guid>(carrier.Id, carrier.Name);

    public static DeliveryCarrierResponse ToDeliveryCarrierResponse(this DeliveryCarrier carrier)
        => new DeliveryCarrierResponse(carrier.Id, carrier.Name, carrier.Code);
}
