using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.ViewModels.Category;

public class CategoryListItemViewModel : IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
}
