using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Utils
{
    public class ContentType
    {
        public string Mime { get; }

        public ContentType(string mime)
        {
            Mime = mime;
        }

        public static ContentType Json { get; } = new ContentType("application/json");
        public static ContentType Xml { get; } = new ContentType("application/xml");
        public static ContentType Form { get; } = new ContentType("application/x-www-form-urlencoded");
        public static ContentType Text { get; } = new ContentType("text/plain");
    }

    public class ContentTypes : IEnumerable<ContentType>
    {
        public IEnumerator<ContentType> GetEnumerator()
        {
            yield return ContentType.Json;
            yield return ContentType.Xml;
            yield return ContentType.Form;
            yield return ContentType.Text;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ContentType>)this).GetEnumerator();
        }
    }
}
