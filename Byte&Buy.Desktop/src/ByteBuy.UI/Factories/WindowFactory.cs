using System;
using Avalonia.Controls;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;

namespace ByteBuy.UI.Factories;

public class WindowFactory
{
    private readonly Func<ApplicationWindowNames, Window> _windowFactory;
    private readonly Func<ApplicationWindowNames, WindowViewModel> _viewModelFactory;
    
    public WindowFactory(Func<ApplicationWindowNames, WindowViewModel> viewModelFactory,
        Func<ApplicationWindowNames, Window> windowFactory)
    {
        _windowFactory = windowFactory;
        _viewModelFactory = viewModelFactory;
    }
    
    public Window GetWindow(ApplicationWindowNames windowName) 
        => _windowFactory.Invoke(windowName);
    
    public WindowViewModel GetWindowViewModel(ApplicationWindowNames name)
        => _viewModelFactory.Invoke(name);
}