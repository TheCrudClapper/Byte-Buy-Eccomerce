using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ByteBuy.UI.ModelsUI.Permission;

public partial class PermissionListItemViewModel : ObservableObject, IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Name { get; set; } = null!;

    [ObservableProperty] private bool _isSelected;

    private bool _isGranted;
    private bool _isRevoked;

    public bool IsGranted
    {
        get => _isGranted;
        set
        {
            if (value != _isGranted)
            {
                if (value)
                {
                    _isRevoked = false;
                    OnPropertyChanged(nameof(IsRevoked));
                }

                _isGranted = value;
                OnPropertyChanged(nameof(IsGranted));
            }
        }
    }

    public bool IsRevoked
    {
        get => _isRevoked;
        set
        {
            if (value != _isRevoked)
            {
                if (value)
                {
                    _isGranted = false;
                    OnPropertyChanged(nameof(IsGranted));
                }

                _isRevoked = value;
                OnPropertyChanged(nameof(IsRevoked));
            }
        }
    }
}