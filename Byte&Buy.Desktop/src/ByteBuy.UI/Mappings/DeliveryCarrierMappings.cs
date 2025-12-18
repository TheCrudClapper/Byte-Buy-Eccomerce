using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.UI.ModelsUI.Delivery;

namespace ByteBuy.UI.Mappings;

public static class DeliveryCarrierMappings
{
    public static DeliveryCarrierListItem ToListItem(this DeliveryCarrierResponse response, int index)
    {
        return new DeliveryCarrierListItem
        {
            Code = response.Code,
            Id = response.Id,
            Name = response.Name,
            RowNumber = index + 1
        };
    }
}
