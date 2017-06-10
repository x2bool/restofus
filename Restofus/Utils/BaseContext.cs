using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Utils
{
    public class BaseContext : ReactiveObject
    {
        IResolver resolver;
        IDictionary<string, Type> named;
        IDictionary<Type, object> cache;

        public I18N I18N
        {
            get => resolver.Resolve<I18N>();
        }

        ContextAccessor self;
        public ContextAccessor Self => self;

        public BaseContext(IResolver resolver)
        {
            this.resolver = resolver;

            this.named = new Dictionary<string, Type>();
            this.cache = new Dictionary<Type, object>();

            self = new ContextAccessor(this);
        }

        public T Get<T>()
        {
            var type = typeof(T);

            if (!cache.TryGetValue(type, out object val))
            {
                val = resolver.Resolve<T>();
                cache.Add(type, val);
            }

            return (T)val;
        }

        public class ContextAccessor
        {
            BaseContext context;

            public ContextAccessor(BaseContext context)
            {
                this.context = context;
            }

            public object this[string name]
            {
                get
                {
                    if (!context.named.TryGetValue(name, out Type type))
                    {
                        type = context.resolver.ResolveType(name);
                        context.named.Add(name, type);
                    }

                    if (!context.cache.TryGetValue(type, out object val))
                    {
                        val = context.resolver.Resolve(type);
                        context.cache.Add(type, val);
                    }

                    return val;
                }
            }
        }
    }
}
