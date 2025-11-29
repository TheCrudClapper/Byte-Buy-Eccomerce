using System;
using System.Threading.Tasks;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelSingle : PageViewModel
{
    protected bool IsEditMode = false;
    protected Guid? EditingItemId = Guid.Empty;

    protected ViewModelSingle(AlertViewModel alert) : base(alert) { }
    
    [RelayCommand]
    protected abstract Task Save();
    
    [RelayCommand]
    protected abstract void Clear();
}