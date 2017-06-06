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
using Restofus.Components.Http;

namespace Restofus.Pads
{
    public class RequestPad : UserControl<RequestPad.Context>
    {
        public RequestPad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : ReactiveObject
        {
            RequestDispatcher httpDispatcher;

            public Context(
                I18N i18n,
                RequestDispatcher httpDispatcher,
                QueryEditor.Context queryEditorContext,
                HeadersEditor.Context headersEditorContext)
            {
                this.httpDispatcher = httpDispatcher;

                QueryEditorContext = queryEditorContext;
                HeadersEditorContext = headersEditorContext;

                I18N = i18n;

                RequestMethods = new RequestMethods();

                SendButtonCommand = ReactiveCommand.CreateAsyncTask(_ =>
                {
                    return httpDispatcher.Dispatch(BuildRequest());
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

            public I18N I18N { get; }

            ReactiveRequest BuildRequest()
            {
                return new ReactiveRequest
                {
                    Method = RequestMethods.Selected.Clone(),
                    Address = UrlInputText
                };
            }
        }

        public class RequestMethods : ReactiveMethodCollection
        {
            ReactiveMethod selected;
            public ReactiveMethod Selected
            {
                get => selected;
                set => this.RaiseAndSetIfChanged(ref selected, value);
            }

            public RequestMethods()
            {
                AddRange(CreateDefault());
                Selected = this[0];
            }
        }
    }
    
}
