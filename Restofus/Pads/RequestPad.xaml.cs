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

        public class Context : BaseContext
        {
            public RequestEditor.Context RequestEditorContext { get; }

            public Context(
                RequestEditor.Context requestEditorContext)
            {
                RequestEditorContext = requestEditorContext;

                //SendButtonCommand = ReactiveCommand.CreateAsyncTask(_ => {
                //    var request = BuildHttpRequest();
                //    httpDispatcher.Send(request);
                //    return Task.CompletedTask;
                //});
            }
            
            HttpRequestMessage BuildHttpRequest()
            {
                return new HttpRequestMessage(
                    RequestEditorContext.RequestMethods.Selected, RequestEditorContext.UrlInputText);
            }
        }
    }
    
}
