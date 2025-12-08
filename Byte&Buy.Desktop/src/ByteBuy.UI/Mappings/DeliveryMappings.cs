using ByteBuy.Core.DTO.Delivery;
using ByteBuy.UI.ModelsUI.Delivery;

namespace ByteBuy.UI.Mappings;

public static class DeliveryMappings
{
    public static DeliveryListItem ToListItem(this DeliveryListResponse dto, int index)
    {
        return new DeliveryListItem
        {
            Amount = dto.Amount,
            Currency = dto.Currency,
            Id = dto.Id,
            Name = dto.Name,
            RowNumber = index + 1
        };
    }
}
