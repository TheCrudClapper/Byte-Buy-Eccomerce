using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelSingle : PageViewModel
{
    [ObservableProperty]
    protected bool _isEditMode = false;

    protected Guid? EditingItemId = Guid.Empty;

    protected ViewModelSingle(AlertViewModel alert) : base(alert) { }

    [RelayCommand]
    protected abstract Task Save();

    [RelayCommand]
    protected abstract void Clear();
}