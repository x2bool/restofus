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
using Restofus.Utils;
using System.Reflection;

namespace Restofus
{
    public class App : Application
    {
        static void Main(string[] args)
        {
            // configure ioc
            var provider = ServiceResolver.Build(new ServiceCollection());

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
    }
}
