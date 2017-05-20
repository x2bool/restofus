using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System;
using System.Collections;
using ReactiveUI;
using System.Reactive;
using Restofus.Components;
using System.Threading.Tasks;
using System.Net.Http;
using Restofus.Utils;

namespace Restofus.Pads
{
    public class RequestPad : UserControl
    {
        public RequestPad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context
        {
            public Context(
                HttpDispatcher httpDispatcher)
            {
                SendButtonCommand = ReactiveCommand.CreateAsyncTask(_ => {
                    var request = BuildHttpRequest();
                    httpDispatcher.Send(request);
                    return Task.CompletedTask;
                });
            }

            public string UrlInputText { get; set; } = "";

            public string SendButtonText { get; } = "Send";

            public ReactiveCommand<Unit> SendButtonCommand { get; }

            public RequestMethods RequestMethods { get; } = new RequestMethods();
            
            HttpRequestMessage BuildHttpRequest()
            {
                return new HttpRequestMessage(RequestMethods.Selected, UrlInputText);
            }
        }
    }

    public class RequestMethods : ReactiveList<HttpMethod>
    {
        public HttpMethod Selected { get; set; }

        public RequestMethods()
        {
            AddRange(new HttpMethods());

            Selected = this[0];
        }
    }
    
}
