using System;
using Avalonia;
using ByteBuy.Desktop.Extensions;
using Microsoft.Extensions.Hosting;

namespace ByteBuy.Desktop
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder();
            
            builder.Services.RegisterViewModels();
            
            var host = builder.Build();
            
            BuildAvaloniaAppWithDi(host)
                .StartWithClassicDesktopLifetime(args);
        }

       
        // Avalonia configuration, don't remove; also used by visual designer.
        //Designer
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();

        //Runtime
        public static AppBuilder BuildAvaloniaAppWithDi(IHost host) =>
            AppBuilder
                .Configure(() => new App(host))
                .UsePlatformDetect()
                .LogToTrace();
    }
}
