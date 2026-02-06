using ByteBuy.Services.DTO.Order.OrderLine;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels.Order.OrderLine;

public class SaleOrderLineViewModel : OrderLineViewModel
{
    public MoneyViewModel PricePerItem { get; }
    public SaleOrderLineViewModel(UserSaleOrderLineResponse dto) : base(dto)
    {
        PricePerItem = new MoneyViewModel(dto.PricePerItem);
    }
}
