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
using Restofus.Pads.Utils;

namespace Restofus.Pads
{
    public class RequestPad : UserControl<RequestPad.Context>
    {
        public RequestPad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            HttpDispatcher httpDispatcher;
            RequestBuilder requestBuilder;

            public Context(
                RequestBuilder requestBuilder,
                HttpDispatcher httpDispatcher,
                QueryEditor.Context queryEditorContext,
                HeadersEditor.Context headersEditorContext)
            {
                this.httpDispatcher = httpDispatcher;
                this.requestBuilder = requestBuilder;

                QueryEditorContext = queryEditorContext;
                HeadersEditorContext = headersEditorContext;

                RequestMethods = new RequestMethods();
                SendButtonCommand = ReactiveCommand.CreateAsyncTask(_ =>
                {
                    var request = requestBuilder.BuildFromContext(this);
                    return httpDispatcher.Dispatch(request);
                });
            }

            string urlInputText;
            public string UrlInputText
            {
                get => urlInputText;
                set => this.RaiseAndSetIfChanged(ref urlInputText, value);
            }

            public RequestMethods RequestMethods { get; } = new RequestMethods();

            public ReactiveCommand<Unit> SendButtonCommand { get; }

            public QueryEditor.Context QueryEditorContext { get; }

            public HeadersEditor.Context HeadersEditorContext { get; }
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
