using ByteBuy.UI.ModelsUI.Money;
using ByteBuy.UI.ModelsUI.Order;

namespace ByteBuy.UI.Mappings;

public static class OrderMappings
{
    public static OrderListItem ToListItem(this CompanyOrderListResponse dto, int index)
    {
        return new OrderListItem()
        {
            Id = dto.Id,
            RowNumber = index + 1,
            Status = dto.Status,
            BuyerEmail = dto.BuyerEmail,
            BuyerFullName = dto.BuyerFullName,
            LinesCount = dto.LinesCount,
            PurchaseDate = dto.PurchasedDate,
            TotalCost = $"{dto.TotalCost.Amount} {dto.TotalCost.Currency}"
        };
    }
}
