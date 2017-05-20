using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Restofus.Utils
{
    public class HttpMethods : IEnumerable<HttpMethod>
    {
        IEnumerator<HttpMethod> IEnumerable<HttpMethod>.GetEnumerator()
        {
            yield return HttpMethod.Get;
            yield return HttpMethod.Post;
            yield return HttpMethod.Put;
            yield return HttpMethod.Delete;
            yield return new HttpMethod("PATCH");
            yield return HttpMethod.Options;
            yield return HttpMethod.Head;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<HttpMethod>)this).GetEnumerator();
        }
    }
}
