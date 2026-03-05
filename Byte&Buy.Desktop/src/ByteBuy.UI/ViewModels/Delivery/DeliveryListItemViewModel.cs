using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.ViewModels.Delivery;

public class DeliveryListItemViewModel : IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal Amount { get; set; }
}
