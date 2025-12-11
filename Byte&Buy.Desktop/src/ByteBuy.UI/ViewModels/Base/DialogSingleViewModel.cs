using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DialogHostAvalonia;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class DialogSingleViewModel(string dialogTitle)
    : DialogViewModel(dialogTitle)
{
    [ObservableProperty]
    private string _error = string.Empty;

    [ObservableProperty]
    protected bool _isEditMode = false;

    protected Guid? EditingItemId = Guid.Empty;

    protected abstract Task<bool> UpdateItem();
    protected abstract Task<bool> AddItem();
    public abstract Task InitializeForEdit(Guid id);

    [RelayCommand]
    protected async Task SaveAsync()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        var ok = IsEditMode
            ? await UpdateItem()
            : await AddItem();

        if (ok)
            DialogHost.Close("MainDialogHost", ok);
    }
}
