using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.Views;

namespace ByteBuy.UI.Navigation;

public class WindowService : IWindowService
{
    private readonly WindowFactory _windowFactory;
    public WindowService(WindowFactory windowFactory)
    {
        _windowFactory = windowFactory;
    }

    //This methods allows only showing new window
    //Initialization of DataContext (given view model) needs to be done in window's constructor (for now)
    public void ShowWindow(ApplicationWindowNames name, bool hideOldOne)
    {
        if (Application.Current?.ApplicationLifetime
           is not IClassicDesktopStyleApplicationLifetime lifetime)
            return;

        var window = _windowFactory.GetWindow(name);

        window.Show();
        if(lifetime.MainWindow is Window windowToClose)
        {
            if (hideOldOne)
                windowToClose.Hide();
            else
                windowToClose.Close();
        }
        lifetime.MainWindow = window;
    }

    public void ShutdownApplication()
    {
        if (Application.Current?.ApplicationLifetime
           is not IClassicDesktopStyleApplicationLifetime lifetime)
            return;

        lifetime.Shutdown();
    }
}
