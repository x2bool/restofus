using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using System;
using System.Net.Http;

namespace Restofus.Pads
{
    public class RequestEditor : UserControl<RequestEditor.Context>
    {
        Button sendRequestButton;

        public RequestEditor()
        {
            AvaloniaXamlLoader.Load(this);

            sendRequestButton = this.FindControl<Button>(nameof(sendRequestButton));
            sendRequestButton.Click += (_, e) =>
            {
                WithContext(context =>
                {
                    context.SendRequest();
                });
            };
        }

        public class Context : BaseContext
        {
            string urlInputText;
            public string UrlInputText
            {
                get => urlInputText;
                set => this.RaiseAndSetIfChanged(ref urlInputText, value);
            }

            public RequestMethods RequestMethods { get; } = new RequestMethods();

            string requestBodyText;
            public string RequestBodyText
            {
                get => requestBodyText;
                set => this.RaiseAndSetIfChanged(ref requestBodyText, value);
            }

            string requestHeadersText;
            public string RequestHeadersText
            {
                get => requestHeadersText;
                set => this.RaiseAndSetIfChanged(ref requestHeadersText, value);
            }
            
            public event EventHandler SendingRequest;
            public void SendRequest()
            {
                SendingRequest?.Invoke(this, null);
            }
        }

        public class RequestMethods : ReactiveList<HttpMethod>
        {
            HttpMethod selected;
            public HttpMethod Selected
            {
                get => selected;
                set => this.RaiseAndSetIfChanged(ref selected, value);
            }

            public RequestMethods()
            {
                AddRange(new HttpMethods());

                Selected = this[0];
            }
        }
    }
}
