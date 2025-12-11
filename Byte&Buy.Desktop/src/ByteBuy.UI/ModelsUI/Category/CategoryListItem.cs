
using ByteBuy.UI.ModelsUI.Abstractions;
using System;

namespace ByteBuy.UI.ModelsUI.Category;

public class CategoryListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
}
