using ByteBuy.Services.DTO.Order.Enums;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels.Dashboard;

public class OrderDashboardViewModel
{
    public string BuyerEmail { get; set; }
    public MoneyViewModel Price { get; set; }
    public int LinesCount { get; set; }
    public string PurchaseDate { get; set; }
    public string Status { get; set; }

    public OrderDashboardViewModel()
    {
        
    }
}
