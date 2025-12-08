using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels;
using ByteBuy.UI.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.Navigation;

public class NavigationService : INavigationService
{
    private readonly MainWindowViewModel _main;
    private readonly PageFactory _pageFactory;
    private readonly WindowFactory _windowFactory;
    public NavigationService(MainWindowViewModel main,
        PageFactory pageFactory, WindowFactory windowFactory)
    {
        _main = main;
        _pageFactory = pageFactory;
        _windowFactory = windowFactory;
    }

    public void NavigateTo(ApplicationPageNames page, Action<PageViewModel>? init = null)
    {
        var pageVm = _pageFactory.GetPageViewModel(page);
        init?.Invoke(pageVm);
        _main.CurrentPage = pageVm;
    }

    public async Task NavigateToAsync(ApplicationPageNames page, Func<PageViewModel, Task>? init = null)
    {
        var pageVm = _pageFactory.GetPageViewModel(page);
        if (init != null)
            await init(pageVm);
        _main.CurrentPage = pageVm;
    }


    public void OpenWindow(ApplicationWindowNames window, Action<WindowViewModel>? init = null)
    {
        var windowVm = _windowFactory.GetWindowViewModel(window);
        var windowView = _windowFactory.GetWindow(window);

        init?.Invoke(windowVm);

        windowView.DataContext = windowVm;
        windowView.Show();
    }

    public async Task OpenWindowAsync(ApplicationWindowNames window, Func<WindowViewModel, Task>? init = null)
    {
        var windowVm = _windowFactory.GetWindowViewModel(window);
        var windowView = _windowFactory.GetWindow(window);

        if (init != null)
            await init.Invoke(windowVm);

        windowView.DataContext = windowVm;
        windowView.Show();
    }
}
