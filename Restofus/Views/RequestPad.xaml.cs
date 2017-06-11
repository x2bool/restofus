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

namespace Restofus.Views
{
    public class RequestPad : UserControl<RequestPad.Context>
    {
        public RequestPad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            IDisposable requestSubscription;
            IDisposable headersSubscription;
            
            Dispatcher httpDispatcher;

            public Context(
                IResolver resolver,
                Dispatcher httpDispatcher,
                RequestProvider requestProvider) : base (resolver)
            {
                this.httpDispatcher = httpDispatcher;
                
                requestSubscription = requestProvider.WhenRequestReady()
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(ObserveRequest);

                Methods = ReactiveMethodCollection.CreateDefault();

                SendCommand = ReactiveCommand.CreateAsyncTask(SendRequest);
            }

            void ObserveRequest(ReactiveRequest request)
            {
                Request = request;

                headersSubscription?.Dispose();
                headersSubscription = request.WhenAnyValue(x => x.Headers)
                    .Subscribe(ObserveHeaders);
            }

            void ObserveHeaders(ReactiveHeaderCollection headers)
            {
                var editor = Get<HeadersEditor.Context>();
                editor.Headers = headers;
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
        }
        
    }
    
}
