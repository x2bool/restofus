using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Restofus.Pads.Utils
{
    public class RequestBuilder
    {
        public HttpRequestMessage BuildFromContext(RequestPad.Context context)
        {
            var request = new HttpRequestMessage(
                context.RequestMethods.Selected,
                context.UrlInputText);
            
            return request;
        }
    }
}
