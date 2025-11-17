using ByteBuy.Desktop.ViewModels;
using ByteBuy.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Desktop.Extensions;

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