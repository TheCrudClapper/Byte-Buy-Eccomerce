using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.ViewModels.DeliveryCarriers;

public class DeliveryCarrierListItemViewModel : IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}
