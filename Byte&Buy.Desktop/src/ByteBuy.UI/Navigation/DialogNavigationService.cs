using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using DialogHostAvalonia;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.Navigation;

public class DialogNavigationService : IDialogNavigationService
{
    private DialogFactory _dialogFactory;

    public DialogNavigationService(DialogFactory dialogFactory)
        => _dialogFactory = dialogFactory;

    public object? OpenDialog(ApplicationDialogNames dialog, Action<PageViewModel>? init = null)
    {
        var viewModel = _dialogFactory.GetDialogViewModel(dialog);
        var view = _dialogFactory.GetView(dialog);

        if (init != null)
            init.Invoke(viewModel);

        view.DataContext = viewModel;

        var result = DialogHost.Show(view, "MainDialogHost");
        return result;
    }

    public async Task<object?> OpenDialogAsync(ApplicationDialogNames dialog, Func<PageViewModel, Task>? init = null)
    {
        var viewModel = _dialogFactory.GetDialogViewModel(dialog);
        var view = _dialogFactory.GetView(dialog);

        if (init != null)
            await init.Invoke(viewModel);

        view.DataContext = viewModel;

        var result = await DialogHost.Show(view, "MainDialogHost");
        return result;
    }
}
