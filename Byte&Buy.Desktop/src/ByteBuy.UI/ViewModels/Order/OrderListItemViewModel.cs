using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.ViewModels.Order;

public class OrderListItemViewModel : IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Status { get; set; } = null!;
    public string PurchaseDate { get; set; } = null!;
    public int LinesCount { get; set; }
    public string BuyerEmail { get; set; } = null!;
    public string BuyerFullName { get; set; } = null!;
    public string TotalCost { get; set; } = null!;
}
