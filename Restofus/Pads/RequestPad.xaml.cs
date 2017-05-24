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

            public RequestEditor.Context RequestEditorContext { get; }

            public Context(
                RequestEditor.Context requestEditorContext,
                RequestBuilder requestBuilder,
                HttpDispatcher httpDispatcher)
            {
                this.httpDispatcher = httpDispatcher;
                this.requestBuilder = requestBuilder;

                RequestEditorContext = requestEditorContext;
                RequestEditorContext.SendingRequest += HandleSendingRequest;
            }

            void HandleSendingRequest(object sender, EventArgs e)
            {
                var request = requestBuilder.BuildFromContext(this);
                httpDispatcher.Dispatch(request);
            }

            public override void Dispose()
            {
                RequestEditorContext.SendingRequest -= HandleSendingRequest;

                base.Dispose();
            }
        }
    }
    
}
