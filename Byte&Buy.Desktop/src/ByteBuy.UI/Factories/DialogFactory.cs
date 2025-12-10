using Avalonia.Controls;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using System;

namespace ByteBuy.UI.Factories;

public class DialogFactory
{
    private readonly Func<ApplicationDialogNames, DialogViewModel> _viewModelFactory;
    private readonly Func<ApplicationDialogNames, UserControl> _viewFactory;

    public DialogFactory(
        Func<ApplicationDialogNames, DialogViewModel> viewModelFactory,
        Func<ApplicationDialogNames, UserControl> viewFactory)
    {
        _viewModelFactory = viewModelFactory;
        _viewFactory = viewFactory;
    }

    public UserControl GetView(ApplicationDialogNames dialogName)
        => _viewFactory.Invoke(dialogName);

    public DialogViewModel GetDialogViewModel(ApplicationDialogNames dialogName)
        => _viewModelFactory.Invoke(dialogName);
}

