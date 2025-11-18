using ByteBuy.Desktop.Views;
using ByteBuy.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using LoginWindowViewModel = ByteBuy.UI.ViewModels.LoginWindowViewModel;

namespace ByteBuy.UI.Extensions;

public static class DependencyInjection
{
    /// <summary>
    /// Registers view models in DI container
    /// </summary>
    /// <param name="services">A collection with DI services</param>
    /// <returns></returns>
    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<LoginWindow>();
        services.AddSingleton<MainWindow>();
        services.AddTransient<LoginWindowViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        return services;
    }
}