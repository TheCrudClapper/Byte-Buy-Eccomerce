using ByteBuy.UI.ModelsUI.Abstractions;
using System;

namespace ByteBuy.UI.ModelsUI.Items;

public class ItemListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public int ImagesCount { get; set; }
    public int StockQuantity { get; set; }
    public string ConditionName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}
