using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.Navigation;

public partial class NavigationService : ObservableObject, INavigationService
{
    private readonly PageFactory _pageFactory;
    private readonly WindowFactory _windowFactory;

    [ObservableProperty]
    public PageViewModel? _currentPage;
     
    public NavigationService(PageFactory pageFactory,
        WindowFactory windowFactory)
    {
        _pageFactory = pageFactory;
        _windowFactory = windowFactory;
        _currentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Dashboard);
    }

    public void NavigateTo(ApplicationPageNames page, Action<PageViewModel>? init = null)
    {
        var pageVm = _pageFactory.GetPageViewModel(page);
        init?.Invoke(pageVm);
        CurrentPage = pageVm;
    }

    public async Task NavigateToAsync(ApplicationPageNames page, Func<PageViewModel, Task>? init = null)
    {
        var pageVm = _pageFactory.GetPageViewModel(page);
        if (init != null)
            await init(pageVm);
        CurrentPage = pageVm;
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
