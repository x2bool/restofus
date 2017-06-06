using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Restofus.Utils
{
    public class HttpClient<T> : HttpClient where T : class
    {
        public HttpClient(HttpClientHandler handler) : base(handler)
        {

        }
    }
}
