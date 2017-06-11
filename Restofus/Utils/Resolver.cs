using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Restofus.Components;
using Restofus.Networking;
using Restofus.Views;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Restofus.Utils
{
    public class Resolver : IResolver
    {
        static IDictionary<string, Type> named;
        static IServiceCollection services;
        static IServiceProvider provider;

        public T Resolve<T>()
        {
            return provider.GetService<T>();
        }

        public object Resolve(Type type)
        {
            return provider.GetService(type);
        }

        public object Resolve(string name)
        {
            return provider.GetService(named[name]);
        }

        public Type ResolveType(string name)
        {
            return named[name];
        }

        static void Add<T>() where T : class
        {
            services.AddTransient<T>();
        }

        static void Add<T>(string name) where T : class
        {
            named.Add(name, typeof(T));
            services.AddTransient<T>();
        }

        static void Add<T>(Func<T> factory) where T : class
        {
            services.AddTransient(_ => factory());
        }

        static void Add<T>(Func<T> factory, string name) where T : class
        {
            named.Add(name, typeof(T));
            services.AddTransient(_ => factory());
        }

        static void AddSingleton<T>() where T : class
        {
            services.AddSingleton<T>();
        }

        static void AddSingleton<T>(Func<T> factory) where T : class
        {
            services.AddSingleton(_ => factory());
        }

        public static IResolver Build()
        {
            if (provider == null)
            {
                named = new Dictionary<string, Type>();
                services = new ServiceCollection();

                Register();

                provider = services.BuildServiceProvider();
            }

            return new Resolver();
        }

        static void Register()
        {
            Add(() => Build());
            
            Add<RequestSerializer>();

            AddSingleton<I18N>();
            AddSingleton<Navigator>();
            AddSingleton<Dispatcher>();
            AddSingleton(() => new HttpClient<Dispatcher>(
                 new HttpClientHandler()
                 {
                     AllowAutoRedirect = false
                 }));

            AddSingleton<RequestProvider>();
            AddSingleton<ResponseProvider>();

            Add<MainWindow.Context>(nameof(MainWindow));
            Add<NavigationPad.Context>(nameof(NavigationPad));
            Add<RequestPad.Context>(nameof(RequestPad));
            Add<ResponsePad.Context>(nameof(ResponsePad));

            Add<QueryEditor.Context>(nameof(QueryEditor));
            Add<HeadersEditor.Context>(nameof(HeadersEditor));
            Add<HeadersViewer.Context>(nameof(HeadersViewer));
        }

    }
}
