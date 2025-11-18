using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ByteBuy.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LoginWindow = ByteBuy.UI.Views.LoginWindow;

namespace ByteBuy.UI
{
    public partial class App : Application
    {
        public static IHost Host { get; private set; }

        public App(){}

        public App(IHost host)
        {
            Host = host;
        }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        
        public override void OnFrameworkInitializationCompleted()
        {
            if (Design.IsDesignMode) return;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = Host.Services.GetRequiredService<MainWindow>();
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