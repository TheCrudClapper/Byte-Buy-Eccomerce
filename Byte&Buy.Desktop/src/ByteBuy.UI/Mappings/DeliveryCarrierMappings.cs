using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.UI.ViewModels.DeliveryCarriers;

namespace ByteBuy.UI.Mappings;

public static class DeliveryCarrierMappings
{
    public static DeliveryCarrierListItemViewModel ToListItem(this DeliveryCarrierResponse response, int index)
    {
        return new DeliveryCarrierListItemViewModel
        {
            Code = response.Code,
            Id = response.Id,
            Name = response.Name,
            RowNumber = index
        };
    }
}
