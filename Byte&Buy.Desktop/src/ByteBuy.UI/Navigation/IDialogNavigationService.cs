using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.Navigation;

public interface IDialogNavigationService
{
    Task<object?> OpenDialogAsync(ApplicationDialogNames dialog,
        Func<PageViewModel, Task>? init = null);

    object? OpenDialog(ApplicationDialogNames dialog,
        Action<PageViewModel>? init = null);
}