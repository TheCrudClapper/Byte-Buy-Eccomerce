using ByteBuy.UI.ModelsUI.Abstractions;
using System;

namespace ByteBuy.UI.ModelsUI.Delivery;

public class DeliveryCarrierListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
