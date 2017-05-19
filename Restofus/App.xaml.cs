using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Logging.Serilog;
using Avalonia.Themes.Default;
using Avalonia.Markup.Xaml;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Restofus.Pads;
using System.Net.Http;
using Restofus.Components;

namespace Restofus
{
    public class App : Application
    {
        static void Main(string[] args)
        {
            // configure ioc
            var provider = Provider.Build(new ServiceCollection());

            // run application
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .Start<MainWindow>(() => provider.GetService<MainWindow.Context>());
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        class Provider : IServiceProvider
        {
            static IServiceProvider provider;

            public object GetService(Type serviceType)
            {
                return provider.GetService(serviceType);
            }

            public T GetService<T>()
            {
                return provider.GetService<T>();
            }

            public static IServiceProvider Build(IServiceCollection services)
            {
                if (provider == null)
                {
                    services.AddSingleton<IServiceProvider, Provider>();

                    services.AddSingleton<HttpClient>();
                    services.AddSingleton<HttpDispatcher>();

                    services.AddTransient<NavigationPad.Context>();
                    services.AddTransient<RequestPad.Context>();
                    services.AddTransient<ResponsePad.Context>();
                    services.AddTransient<MainWindow.Context>();

                    provider = services.BuildServiceProvider();
                }

                return new Provider();
            }
        }
    }
}
