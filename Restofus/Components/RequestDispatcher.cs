using ReactiveUI;
using Restofus.Components.Http;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restofus.Components
{
    public class RequestDispatcher
    {
        HttpClient<RequestDispatcher> httpClient;

        public event EventHandler<ReactiveRequest> Request;
        public event EventHandler<ReactiveResponse> Response;
        public event EventHandler<Exception> Exception;

        public RequestDispatcher(
            HttpClient<RequestDispatcher> httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task Dispatch(ReactiveRequest request)
        {
            Task<HttpResponseMessage> sendTask = null;

            try
            {
                Request?.Invoke(this, request);
                sendTask = httpClient.SendAsync(Convert(request));
            }
            catch (Exception e)
            {
                Exception?.Invoke(this, e);
            }

            sendTask?.ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Exception?.Invoke(this, task.Exception);
                }
                else
                {
                    Response?.Invoke(this, Convert(task.Result));
                }
            });

            return Task.CompletedTask;
        }

        static HttpRequestMessage Convert(ReactiveRequest request)
        {
            var httpRequest = new HttpRequestMessage(
                new HttpMethod(request.Method.Name), request.Address);

            foreach (var header in request.Headers)
            {
                httpRequest.Headers.Add(header.Name, header.Value);
            }

            return httpRequest;
        }

        static ReactiveResponse Convert(HttpResponseMessage httpResponse)
        {
            return FromHttpResponse(httpResponse);
        }
        
        static ReactiveResponse FromHttpResponse(HttpResponseMessage httpResponse)
        {
            var response = new ReactiveResponse();

            response.StatusCode = (int)httpResponse.StatusCode;
            response.Headers = FromHttpHeaders(httpResponse.Headers);
            response.Content = FromHttpContent(httpResponse.Content);

            return response;
        }

        static ReactiveHeaderCollection FromHttpHeaders(HttpHeaders httpHeaders)
        {
            var collection = new ReactiveHeaderCollection();

            collection.AddRange(
                httpHeaders.SelectMany(
                    h => h.Value.Select(v => new ReactiveHeader(h.Key, v))));

            return collection;
        }

        static ReactiveResponseContent FromHttpContent(HttpContent content)
        {
            var responseContent = new ReactiveResponseContent();

            var memoryStream = new MemoryStream();
            content.CopyToAsync(memoryStream).ContinueWith(task =>
            {
                memoryStream.Flush();
                memoryStream.Position = 0;
                responseContent.MemoryStream = memoryStream;
            });

            return responseContent;
        }

    }
}
