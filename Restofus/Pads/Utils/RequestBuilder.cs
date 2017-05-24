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
            var editorContext = context.RequestEditorContext;

            var request = new HttpRequestMessage(
                editorContext.RequestMethods.Selected,
                editorContext.UrlInputText);

            if (!string.IsNullOrEmpty(editorContext.RequestBodyText))
            {
                var body = Encoding.UTF8.GetBytes(editorContext.RequestBodyText);
                request.Content = new ByteArrayContent(body);
            }

            return request;
        }
    }
}
