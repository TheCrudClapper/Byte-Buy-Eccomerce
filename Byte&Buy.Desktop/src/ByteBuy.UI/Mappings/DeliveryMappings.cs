using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.ViewModels.Delivery;

namespace ByteBuy.UI.Mappings;

public static class DeliveryMappings
{
    public static DeliveryListItemViewModel ToListItem(this DeliveryListResponse dto, int index)
    {
        return new DeliveryListItemViewModel
        {
            Amount = dto.Amount,
            Currency = dto.Currency,
            Id = dto.Id,
            Name = dto.Name,
            RowNumber = index
        };
    }

    public static DeliveryOptionViewModel ToDeliveryOption(this DeliveryOptionResponse dto)
    {
        return new DeliveryOptionViewModel
        {
            Carrier = dto.Carrier,
            DeliveryChannel = dto.DeliveryChannel,
            Id = dto.Id,
            Name = dto.Name,
            PriceAndCurrency = $"{dto.Amount} {dto.Currency}"
        };
    }
}
