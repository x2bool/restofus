using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Restofus.Components;
using Restofus.Networking;
using Restofus.Pads;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Restofus.Utils
{
    public class ServiceResolver : IServiceProvider
    {
        static IServiceProvider provider;

        object IServiceProvider.GetService(Type serviceType)
        {
            return provider.GetService(serviceType);
        }

        public static IServiceProvider Build(IServiceCollection services)
        {
            if (provider == null)
            {
                services.AddTransient(_ => Build(services));
                
                services.AddSingleton(_ => new HttpClient<RequestDispatcher>(
                    new HttpClientHandler()
                    {
                        AllowAutoRedirect = false
                    }));
                services.AddSingleton<RequestDispatcher>();
                services.AddTransient<ReactiveRequestSerializer>();

                services.AddSingleton<Navigator>();

                services.AddSingleton<I18N>();

                services.AddTransient<NavigationPad.Context>();
                
                services.AddTransient<QueryEditor.Context>();
                services.AddTransient<HeadersEditor.Context>();
                services.AddTransient<RequestPad.Context>();

                services.AddTransient<HeadersViewer.Context>();
                services.AddTransient<ResponsePad.Context>();

                services.AddTransient<MainWindow.Context>();

                provider = services.BuildServiceProvider();
            }

            return new ServiceResolver();
        }
    }
}
