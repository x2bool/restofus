using ReactiveUI;
using Restofus.Networking;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public event EventHandler<ReactiveRequest> Sending;
        public event EventHandler<ReactiveResponse> Receiving;
        //public event EventHandler<Exception> Exception;

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
                Sending?.Invoke(this, request);

                var httpRequest = Convert(request);
                sendTask = httpClient.SendAsync(httpRequest);
            }
            catch
            {
                //Exception?.Invoke(this, e);
                throw;
            }

            sendTask?.ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    //Exception?.Invoke(this, task.Exception);
                    throw task.Exception;
                }
                else
                {
                    var response = Convert(task.Result);
                    Receiving?.Invoke(this, response);
                }
            });

            return Task.CompletedTask;
        }

        static HttpRequestMessage Convert(ReactiveRequest request)
        {
            var httpRequest = new HttpRequestMessage(
                new HttpMethod(request.Method.Name), request.Url.Address);

            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    httpRequest.Headers.Add(header.Name, header.Value);
                }
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

    public static class RequestDispatcherExtensions
    {
        public static IObservable<ReactiveRequest> GetRequestObservable(
            this RequestDispatcher dispatcher)
        {
            return Observable.FromEventPattern<ReactiveRequest>(
                        h => dispatcher.Sending += h, h => dispatcher.Sending -= h)
                    .Select(e => e.EventArgs);
        }

        public static IObservable<ReactiveResponse> GetResponseObservable(
            this RequestDispatcher dispatcher)
        {
            return Observable.FromEventPattern<ReactiveResponse>(
                        h => dispatcher.Receiving += h, h => dispatcher.Receiving -= h)
                    .Select(e => e.EventArgs);
        }
    }
}
