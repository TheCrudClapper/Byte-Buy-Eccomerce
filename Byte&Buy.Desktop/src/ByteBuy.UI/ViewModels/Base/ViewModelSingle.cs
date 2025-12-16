using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelSingle(AlertViewModel alert)
    : PageViewModel(alert)
{
    [ObservableProperty]
    protected bool _isEditMode = false;

    protected Guid? EditingItemId = Guid.Empty;

    protected abstract Task UpdateItem();
    protected abstract Task AddItem();
    protected virtual Task InitializeAsync() => Task.CompletedTask;

    [RelayCommand]
    protected virtual async Task Save()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        await (IsEditMode switch
        {
            true => UpdateItem(),
            false => AddItem()
        });
    }

    public virtual async Task InitializeForAdd()
    {
        IsEditMode = false;
        EditingItemId = null;
        await InitializeAsync();
    }

    [RelayCommand]
    protected abstract void Clear();
}