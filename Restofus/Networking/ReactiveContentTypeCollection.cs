using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Networking
{
    public class ReactiveContentTypeCollection : ReactiveList<ReactiveContentType>
    {
        public static ReactiveContentTypeCollection CreateDefault()
        {
            var collection = new ReactiveContentTypeCollection();

            collection.Add(new ReactiveContentType("application/json"));
            collection.Add(new ReactiveContentType("application/xml"));
            collection.Add(new ReactiveContentType("application/x-www-form-urlencoded"));
            collection.Add(new ReactiveContentType("text/plain"));

            return collection;
        }
    }
}
