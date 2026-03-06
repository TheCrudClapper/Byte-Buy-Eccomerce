using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Services.Extensions;
using ByteBuy.Services.Handlers;
using ByteBuy.UI.Data;
using ByteBuy.UI.Extensions;
using ByteBuy.UI.Factories;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using Microsoft.Extensions.Configuration;
using ByteBuy.UI.ViewModels.SingleViewModels;
using ByteBuy.UI.Views;
using ByteBuy.UI.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using PageViewModel = ByteBuy.UI.ViewModels.Base.PageViewModel;
using Microsoft.Extensions.Options;
using ByteBuy.Infrastructure.Options;


namespace ByteBuy.UI
{
    public partial class App : Application
    {

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            //Add Configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            //Registering Services
            var services = new ServiceCollection();
            services.AddAuthHeaderHandler();
            services.AddUserInterfaceLayer();
            services.AddServiceLayer();
            services.AddInfrastructureLayer();
            services.AddSingleton<IConfiguration>(configuration);

            services.Configure<ApiEndpointsOptions>(
                configuration.GetSection("ApiEnpoints"));

            services.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
            {
                ApplicationPageNames.Dashboard => x.GetRequiredService<DashboardPageViewModel>(),
                ApplicationPageNames.Employee => x.GetRequiredService<EmployeePageViewModel>(),
                ApplicationPageNames.Employees => x.GetRequiredService<EmployeesPageViewModel>(),
                ApplicationPageNames.Role => x.GetRequiredService<RolePageViewModel>(),
                ApplicationPageNames.Roles => x.GetRequiredService<RolesPageViewModel>(),
                ApplicationPageNames.Profile => x.GetRequiredService<ProfilePageViewModel>(),
                ApplicationPageNames.CompanyInfo => x.GetRequiredService<CompanyInfoPageViewModel>(),
                ApplicationPageNames.PortalUsers => x.GetRequiredService<PortalUsersPageViewModel>(),
                ApplicationPageNames.PortalUser => x.GetRequiredService<PortalUserPageViewModel>(),
                ApplicationPageNames.Categories => x.GetRequiredService<CategoriesPageViewModel>(),
                ApplicationPageNames.Deliveries => x.GetRequiredService<DeliveriesPageViewModel>(),
                ApplicationPageNames.Conditions => x.GetRequiredService<ConditionsPageViewModel>(),
                ApplicationPageNames.Countries => x.GetRequiredService<CountriesPageViewModel>(),
                ApplicationPageNames.Administration => x.GetRequiredService<AdministrationPageViewModel>(),
                ApplicationPageNames.Items => x.GetRequiredService<ItemsPageViewModel>(),
                ApplicationPageNames.Item => x.GetRequiredService<ItemPageViewModel>(),
                ApplicationPageNames.RentOffers => x.GetRequiredService<RentOffersViewModel>(),
                ApplicationPageNames.SaleOffers => x.GetRequiredService<SaleOffersViewModel>(),
                ApplicationPageNames.DeliveryCarriers => x.GetRequiredService<DeliveryCarriersViewModel>(),
                ApplicationPageNames.Orders => x.GetRequiredService<OrdersPageViewModel>(),
                ApplicationPageNames.OrderDetails => x.GetRequiredService<OrderDetailsPageViewModel>(),
                ApplicationPageNames.Rentals => x.GetRequiredService<RentalsPageViewModel>(),
                ApplicationPageNames.RentalDetails => x.GetRequiredService<RentalDetailsPageViewModel>(),
                _ => throw new InvalidOperationException(),
            });

            //Windows ViewModels and Views
            services.AddSingleton<Func<ApplicationWindowNames, WindowViewModel>>(x => name => name switch
            {
                ApplicationWindowNames.Login => x.GetRequiredService<LoginWindowViewModel>(),
                ApplicationWindowNames.Main => x.GetRequiredService<MainWindowViewModel>(),
                _ => throw new InvalidOperationException(),
            });

            services.AddSingleton<Func<ApplicationWindowNames, Window>>(x => name => name switch
            {
                ApplicationWindowNames.Login => x.GetRequiredService<LoginWindow>(),
                ApplicationWindowNames.Main => x.GetRequiredService<MainWindow>(),
                _ => throw new InvalidOperationException(),
            });

            //Dialog ViewModels and Views
            services.AddSingleton<Func<ApplicationDialogNames, DialogViewModel>>(x => name => name switch
            {
                ApplicationDialogNames.Category => x.GetRequiredService<CategoryDialogViewModel>(),
                ApplicationDialogNames.Country => x.GetRequiredService<CountryDialogViewModel>(),
                ApplicationDialogNames.Delivery => x.GetRequiredService<DeliveryDialogViewModel>(),
                ApplicationDialogNames.Condition => x.GetRequiredService<ConditionDialogViewModel>(),
                ApplicationDialogNames.Confirm => x.GetRequiredService<ConfirmationDialogViewModel>(),
                ApplicationDialogNames.Offer => x.GetRequiredService<OfferDialogViewModel>(),
                ApplicationDialogNames.DeliveryCarrier => x.GetRequiredService<DeliveryCarrierDialogViewModel>(),
                _ => throw new InvalidOperationException(),
            });

            services.AddSingleton<Func<ApplicationDialogNames, UserControl>>(x => name => name switch
            {
                ApplicationDialogNames.Category => x.GetRequiredService<CategoryDialogView>(),
                ApplicationDialogNames.Country => x.GetRequiredService<CountryDialogView>(),
                ApplicationDialogNames.Condition => x.GetRequiredService<ConditionDialogView>(),
                ApplicationDialogNames.Delivery => x.GetRequiredService<DeliveryDialogView>(),
                ApplicationDialogNames.Confirm => x.GetRequiredService<ConfirmDialogView>(),
                ApplicationDialogNames.Offer => x.GetRequiredService<OfferDialogView>(),
                ApplicationDialogNames.DeliveryCarrier => x.GetRequiredService<DeliveryCarrierDialogView>(),
                _ => throw new InvalidOperationException(),
            });

            //Register factories
            services.AddSingleton<PageFactory>();
            services.AddSingleton<WindowFactory>();
            services.AddSingleton<DialogFactory>();

            //Register Navigation
            services.AddSingleton<INavigationService>(sp => new NavigationService(
                sp.GetRequiredService<PageFactory>(),
                sp.GetRequiredService<WindowFactory>()
                ));

            //Application LifeTime
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifeTime)
            {
                services.AddSingleton<IClassicDesktopStyleApplicationLifetime>(desktopLifeTime);
            }

            //Top level provider
            services.AddSingleton<Func<TopLevel?>>(x => () =>
            {
                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime topDesktop)
                    return TopLevel.GetTopLevel(topDesktop.MainWindow);

                return null;
            });

            //Register Dialog Navigation
            services.AddSingleton<IDialogService, DialogService>();

            var provider = services.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();

                desktop.MainWindow = provider.GetRequiredService<LoginWindow>();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}