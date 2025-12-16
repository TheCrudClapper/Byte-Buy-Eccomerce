using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.Navigation;

public interface INavigationService : INotifyPropertyChanged
{
    PageViewModel? CurrentPage { get; }
    /// <summary>
    /// Navigate to a page with optional async initialization
    /// </summary>
    Task NavigateToAsync(ApplicationPageNames page, Func<PageViewModel, Task>? init = null);

    /// <summary>
    /// Navigate to a page synchronously with optional initialization
    /// </summary>
    void NavigateTo(ApplicationPageNames page, Action<PageViewModel>? init = null);

    /// <summary>
    /// Open a new window asynchronously with async initialization
    /// </summary>
    Task OpenWindowAsync(ApplicationWindowNames window, Func<WindowViewModel, Task>? init = null);

    /// <summary>
    /// Open a new window with optional initialization
    /// </summary>
    void OpenWindow(ApplicationWindowNames window, Action<WindowViewModel>? init = null);

}
