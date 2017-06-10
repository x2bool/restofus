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
        IDictionary<string, object> cache;

        public I18N I18N
        {
            get => resolver.Resolve<I18N>();
        }

        ContextAccessor self;
        public ContextAccessor Self => self;

        public BaseContext(IResolver resolver)
        {
            this.resolver = resolver;
            this.cache = new Dictionary<string, object>();
            self = new ContextAccessor(this);
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
                    if (!context.cache.TryGetValue(name, out object obj))
                    {
                        obj = context.resolver.Resolve(name);
                        context.cache.Add(name, obj);
                    }
                    return obj;
                }
            }
        }
    }
}
