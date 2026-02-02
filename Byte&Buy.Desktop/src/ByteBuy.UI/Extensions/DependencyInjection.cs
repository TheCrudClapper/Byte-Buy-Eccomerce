using ByteBuy.UI.ViewModels;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using ByteBuy.UI.Views.Dialogs;
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
    public static IServiceCollection AddUserInterfaceLayer(this IServiceCollection services)
    {
        //Singletons
        services.AddSingleton<MainWindowViewModel>();

        //Windows
        services.AddTransient<LoginWindow>();
        services.AddSingleton<MainWindow>();

        //Dialog Views
        services.AddTransient<CategoryDialogView>();
        services.AddTransient<CountryDialogView>();
        services.AddTransient<DeliveryDialogView>();
        services.AddTransient<ConditionDialogView>();
        services.AddTransient<ConfirmDialogView>();
        services.AddTransient<OfferDialogView>();
        services.AddTransient<DeliveryCarrierDialogView>();

        //Alert
        services.AddTransient<AlertViewModel>();

        //Pages View Models
        services.AddTransient<LoginWindowViewModel>();
        services.AddTransient<DashboardPageViewModel>();
        services.AddTransient<EmployeesPageViewModel>();
        services.AddTransient<EmployeePageViewModel>();
        services.AddTransient<ProfilePageViewModel>();
        services.AddTransient<RolesPageViewModel>();
        services.AddTransient<RolePageViewModel>();
        services.AddTransient<PasswordChangeViewModel>();
        services.AddTransient<CompanyInfoPageViewModel>();
        services.AddTransient<PermissionListBoxViewModel>();
        services.AddTransient<PermissionGrantRevokeViewModel>();
        services.AddTransient<PortalUsersPageViewModel>();
        services.AddTransient<PortalUserPageViewModel>();
        services.AddTransient<AdministrationPageViewModel>();
        services.AddTransient<CountriesPageViewModel>();
        services.AddTransient<DeliveriesPageViewModel>();
        services.AddTransient<ConditionsPageViewModel>();
        services.AddTransient<CategoriesPageViewModel>();
        services.AddTransient<ItemsPageViewModel>();
        services.AddTransient<ItemPageViewModel>();
        services.AddTransient<RentOffersViewModel>();
        services.AddTransient<SaleOffersViewModel>();
        services.AddTransient<DeliveryCarriersViewModel>();


        //Dialgos View Models
        services.AddTransient<OfferDialogViewModel>();
        services.AddTransient<CategoryDialogViewModel>();
        services.AddTransient<ConditionDialogViewModel>();
        services.AddTransient<DeliveryDialogViewModel>();
        services.AddTransient<CountryDialogViewModel>();
        services.AddTransient<ConfirmationDialogViewModel>();
        services.AddTransient<DeliveryCarrierDialogViewModel>();
        return services;
    }
}