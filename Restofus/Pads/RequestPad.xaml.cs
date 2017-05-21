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
    public class RequestPad : UserControl<RequestPad.Context>
    {
        public RequestPad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            HttpDispatcher httpDispatcher;

            public RequestEditor.Context RequestEditorContext { get; }

            public Context(
                RequestEditor.Context requestEditorContext,
                HttpDispatcher httpDispatcher)
            {
                this.httpDispatcher = httpDispatcher;

                RequestEditorContext = requestEditorContext;
                RequestEditorContext.SendingRequest += HandleSendingRequest;
            }

            void HandleSendingRequest(object sender, EventArgs e)
            {
                var request = BuildHttpRequest(RequestEditorContext);
                httpDispatcher.Dispatch(request);
            }

            HttpRequestMessage BuildHttpRequest(RequestEditor.Context requestEditorContext)
            {
                return new HttpRequestMessage(
                    requestEditorContext.RequestMethods.Selected, requestEditorContext.UrlInputText);
            }

            public override void Dispose()
            {
                RequestEditorContext.SendingRequest -= HandleSendingRequest;

                base.Dispose();
            }
        }
    }
    
}
