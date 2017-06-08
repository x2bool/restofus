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
using Restofus.Networking;
using System.Reactive.Linq;
using Restofus.Navigation;
using System.IO;

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
            IDisposable navigationSubscription;

            Navigator navigator;
            RequestDispatcher httpDispatcher;
            ReactiveRequestFactory requestFactory;

            public Context(
                I18N i18n,
                Navigator navigator,
                RequestDispatcher httpDispatcher,
                ReactiveRequestFactory requestFactory,
                QueryEditor.Context queryEditorContext,
                HeadersEditor.Context headersEditorContext)
            {
                this.navigator = navigator;
                this.httpDispatcher = httpDispatcher;

                QueryEditorContext = queryEditorContext;
                HeadersEditorContext = headersEditorContext;

                I18N = i18n;
                
                navigationSubscription = navigator
                    .GetNavigationObservable()
                    .Where(f => f != null)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(ObserveNavigation);

                Methods = ReactiveMethodCollection.CreateDefault();

                SendCommand = ReactiveCommand.CreateAsyncTask(SendRequest);
            }

            void ObserveNavigation(ReactiveFile file)
            {
                Request = requestFactory.DeserializeRequest(new FileInfo(file.Path));
            }

            Task SendRequest(object arg)
            {
                return httpDispatcher.Dispatch(Request?.Clone());
            }

            ReactiveRequest request;
            public ReactiveRequest Request
            {
                get => request;
                set => this.RaiseAndSetIfChanged(ref request, value);
            }

            public ReactiveMethodCollection Methods { get; }

            public ReactiveCommand<Unit> SendCommand { get; }

            public QueryEditor.Context QueryEditorContext { get; }

            public HeadersEditor.Context HeadersEditorContext { get; }

            public I18N I18N { get; }
        }
        
    }
    
}
