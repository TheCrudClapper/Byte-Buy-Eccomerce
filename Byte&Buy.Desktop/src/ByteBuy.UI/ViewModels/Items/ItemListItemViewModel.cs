using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.ViewModels.Items;

public class ItemListItemViewModel : IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public int ImagesCount { get; set; }
    public int StockQuantity { get; set; }
    public string ConditionName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}
