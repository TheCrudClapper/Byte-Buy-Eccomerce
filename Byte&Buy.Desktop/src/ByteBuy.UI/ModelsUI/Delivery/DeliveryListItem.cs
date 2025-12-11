using ByteBuy.UI.ModelsUI.Abstractions;
using System;

namespace ByteBuy.UI.ModelsUI.Delivery;

public class DeliveryListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal Amount { get; set; }
}
