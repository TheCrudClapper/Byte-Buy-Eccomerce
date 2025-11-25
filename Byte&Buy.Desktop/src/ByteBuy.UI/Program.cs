using System;
using Avalonia;
using ByteBuy.Services.Extensions;
using ByteBuy.Services.Handlers;
using ByteBuy.UI.Extensions;
using Microsoft.Extensions.Hosting;

namespace ByteBuy.UI
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        // public static void Main(string[] args)
        // {
        //     var builder = Host.CreateApplicationBuilder();
        //
        //     builder.Services.AddServiceLayer();
        //     builder.Services.RegisterViewModels();
        //     builder.Services.AddAuthHeaderHandler();
        //     var host = builder.Build();
        //     
        //     BuildAvaloniaAppWithDi(host)
        //         .StartWithClassicDesktopLifetime(args);
        // }

       
        // Avalonia configuration, don't remove; also used by visual designer.
        //Designer
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseSkia()  
                .LogToTrace();
        
    }
}
