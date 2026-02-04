using ByteBuy.UI.ModelsUI.Abstractions;
using ByteBuy.UI.ModelsUI.Money;
using System;

namespace ByteBuy.UI.ModelsUI.Order;

public class OrderListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Status { get; set; } = null!;
    public DateTime PurchaseDate { get; set; }
    public int LinesCount { get; set; }
    public string BuyerEmail { get; set; } = null!;
    public string BuyerFullName { get; set; } = null!;
    public string TotalCost { get; set; } = null!;
}
