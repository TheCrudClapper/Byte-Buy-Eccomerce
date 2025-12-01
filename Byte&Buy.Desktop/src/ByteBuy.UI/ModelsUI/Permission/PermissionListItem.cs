using ByteBuy.UI.ModelsUI.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ByteBuy.UI.ModelsUI.Permission;

public partial class PermissionListItem : ObservableObject, IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;


    [ObservableProperty]
    private bool _isSelected;
}