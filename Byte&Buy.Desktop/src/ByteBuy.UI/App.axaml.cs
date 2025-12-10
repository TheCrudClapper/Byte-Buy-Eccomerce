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
using ByteBuy.UI.Views;
using ByteBuy.UI.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using PageViewModel = ByteBuy.UI.ViewModels.Base.PageViewModel;


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
            //Registering Services
            var services = new ServiceCollection();
            services.AddAuthHeaderHandler();
            services.RegisterViewModels();
            services.AddServiceLayer();
            services.AddInfrastructureLayer();

            services.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
            {
                ApplicationPageNames.Dashboard => x.GetRequiredService<DashboardPageViewModel>(),
                ApplicationPageNames.Employee => x.GetRequiredService<EmployeePageViewModel>(),
                ApplicationPageNames.Employees => x.GetRequiredService<EmployeesPageViewModel>(),
                ApplicationPageNames.Role => x.GetRequiredService<RolePageViewModel>(),
                ApplicationPageNames.Roles => x.GetRequiredService<RolesPageViewModel>(),
                ApplicationPageNames.Profile => x.GetRequiredService<ProfilePageViewModel>(),
                ApplicationPageNames.Settings => x.GetRequiredService<SettingsPageViewModel>(),
                ApplicationPageNames.CompanyInfo => x.GetRequiredService<CompanyInfoPageViewModel>(),
                ApplicationPageNames.PortalUsers => x.GetRequiredService<PortalUsersPageViewModel>(),
                ApplicationPageNames.PortalUser => x.GetRequiredService<PortalUserPageViewModel>(),
                ApplicationPageNames.Categories => x.GetRequiredService<CategoriesPageViewModel>(),
                ApplicationPageNames.Deliveries => x.GetRequiredService<DeliveriesPageViewModel>(),
                ApplicationPageNames.Conditions => x.GetRequiredService<ConditionsPageViewModel>(),
                ApplicationPageNames.Countries => x.GetRequiredService<CountriesPageViewModel>(),
                ApplicationPageNames.Administration => x.GetRequiredService<AdministrationPageViewModel>(),


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
                _ => throw new InvalidOperationException(),
            });

            services.AddSingleton<Func<ApplicationDialogNames, UserControl>>(x => name => name switch
            {
                ApplicationDialogNames.Category => x.GetRequiredService<CategoryDialogView>(),
                ApplicationDialogNames.Country => x.GetRequiredService<CountryDialogView>(),
                ApplicationDialogNames.Condition => x.GetRequiredService<ConditionDialogView>(),
                ApplicationDialogNames.Delivery => x.GetRequiredService<DeliveryDialogView>(),
                _ => throw new InvalidOperationException(),
            });

            //Register factories
            services.AddSingleton<PageFactory>();
            services.AddSingleton<WindowFactory>();
            services.AddSingleton<DialogFactory>();

            //Register Navigation
            services.AddSingleton<INavigationService>(sp => new NavigationService(
                sp.GetRequiredService<MainWindowViewModel>(),
                sp.GetRequiredService<PageFactory>(),
                sp.GetRequiredService<WindowFactory>()
                ));

            //Register Dialog Navigation
            services.AddSingleton<IDialogNavigationService>(sp => new DialogNavigationService(
                    sp.GetRequiredService<DialogFactory>()));

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