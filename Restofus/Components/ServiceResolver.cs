﻿using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Restofus.Pads;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Restofus.Components
{
    public class ServiceResolver : IServiceProvider
    {
        static IServiceProvider provider;

        object IServiceProvider.GetService(Type serviceType)
        {
            var service = provider.GetService(serviceType);
            ResolveBase(service);
            return service;
        }

        void ResolveBase(object service)
        {
            if (service != null && service is BaseContext)
            {
                var type = service.GetType();

                var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite);

                foreach (var prop in props)
                {
                    if (prop.DeclaringType == typeof(BaseContext))
                    {
                        var value = provider.GetService(prop.PropertyType);
                        prop.SetValue(service, value);
                    }
                    else
                    {
                        var value = prop.GetValue(service);
                        ResolveBase(value);
                    }
                }
            }
        }

        public static IServiceProvider Build(IServiceCollection services)
        {
            if (provider == null)
            {
                services.AddSingleton<HttpClient>();
                services.AddSingleton<HttpDispatcher>();

                services.AddSingleton<I18N>();

                services.AddTransient<NavigationPad.Context>();

                services.AddTransient<RequestEditor.Context>();
                services.AddTransient<RequestPad.Context>();

                services.AddTransient<ResponseViewer.Context>();
                services.AddTransient<ResponsePad.Context>();

                services.AddTransient<MainWindow.Context>();

                provider = services.BuildServiceProvider();
                //services.AddSingleton(provider);
            }

            return new ServiceResolver();
        }
    }
}
