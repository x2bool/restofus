using ReactiveUI;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restofus.Components
{
    public class HttpDispatcher
    {
        HttpClient<HttpDispatcher> httpClient;

        event EventHandler<HttpRequestMessage> RequestEvent;
        public IObservable<HttpRequestMessage> Requests { get; }

        event EventHandler<HttpResponseMessage> ResponseEvent;
        public IObservable<HttpResponseMessage> Responses { get; }

        event EventHandler<HttpRequestException> ExceptionEvent;
        public IObservable<HttpRequestException> Exceptions { get; }

        public HttpDispatcher(
            HttpClient<HttpDispatcher> httpClient)
        {
            this.httpClient = httpClient;
            
            Requests = Observable.FromEventPattern<HttpRequestMessage>(
                h => RequestEvent += h, h => RequestEvent -= h)
                .Select(e => e.EventArgs);

            Responses = Observable.FromEventPattern<HttpResponseMessage>(
                h => ResponseEvent += h, h => ResponseEvent -= h)
                .Select(e => e.EventArgs);

            Exceptions = Observable.FromEventPattern<HttpRequestException>(
                h => ExceptionEvent += h, h => ExceptionEvent -= h)
                .Select(e => e.EventArgs);
        }

        public void Dispatch(HttpRequestMessage request)
        {
            RequestEvent?.Invoke(this, request);

            httpClient.SendAsync(request)
                .ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        ExceptionEvent?.Invoke(this, task.Exception.InnerException as HttpRequestException);
                    }
                    else
                    {
                        ResponseEvent?.Invoke(this, task.Result);
                    }
                });
        }

    }
}
