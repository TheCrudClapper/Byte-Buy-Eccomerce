using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.ViewModels.Permission;

public class PermissionManyListItem : IListItemViewModel
{
    public int RowNumber { get; set;}
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
