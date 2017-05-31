using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Components.Http
{
    public class ReactiveMethodCollection : ReactiveList<ReactiveMethod>
    {
        public static ReactiveMethodCollection CreateDefault()
        {
            var collection = new ReactiveMethodCollection();

            collection.Add(new ReactiveMethod("GET"));
            collection.Add(new ReactiveMethod("POST"));
            collection.Add(new ReactiveMethod("PUT"));
            collection.Add(new ReactiveMethod("DELETE"));
            collection.Add(new ReactiveMethod("PATCH"));
            collection.Add(new ReactiveMethod("OPTIONS"));
            collection.Add(new ReactiveMethod("HEAD"));

            return collection;
        }
    }
}
