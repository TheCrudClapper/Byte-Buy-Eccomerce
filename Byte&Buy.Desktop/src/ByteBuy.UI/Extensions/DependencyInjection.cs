using ByteBuy.UI.ViewModels;
using ByteBuy.UI.ViewModels.Shared;
using Microsoft.Extensions.DependencyInjection;
using DashboardPageViewModel = ByteBuy.UI.ViewModels.DashboardPageViewModel;
using EmployeePageViewModel = ByteBuy.UI.ViewModels.EmployeePageViewModel;
using LoginWindow = ByteBuy.UI.Views.LoginWindow;
using LoginWindowViewModel = ByteBuy.UI.ViewModels.LoginWindowViewModel;
using MainWindow = ByteBuy.UI.Views.MainWindow;
using ProfilePageViewModel = ByteBuy.UI.ViewModels.ProfilePageViewModel;

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
        //Singletons
        services.AddSingleton<MainWindowViewModel>();
        
        //Windows
        services.AddTransient<LoginWindow>();
        services.AddSingleton<MainWindow>();
        
        //Transients
        services.AddTransient<LoginWindowViewModel>();
        services.AddTransient<DashboardPageViewModel>();
        services.AddTransient<EmployeesPageViewModel>();
        services.AddTransient<SettingsPageViewModel>();
        services.AddTransient<EmployeePageViewModel>();
        services.AddTransient<ProfilePageViewModel>();
        services.AddTransient<RolesPageViewModel>();
        services.AddTransient<RolePageViewModel>();
        services.AddTransient<PasswordChangeViewModel>();
        services.AddTransient<AlertViewModel>();
        return services;
    }
}