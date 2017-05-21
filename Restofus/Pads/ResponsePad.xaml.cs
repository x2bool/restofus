using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using System;
using System.Reactive.Linq;

namespace Restofus.Pads
{
    public class ResponsePad : UserControl<ResponsePad.Context>
    {
        public ResponsePad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext, IDisposable
        {
            IDisposable responsesSubscription;

            public ResponseViewer.Context ResponseViewerContext { get; }

            public Context(
                ResponseViewer.Context responseViewerContext,
                I18N i18n,
                HttpDispatcher httpDispatcher)
            {
                ResponseViewerContext = responseViewerContext;

                responsesSubscription = httpDispatcher.Responses
                    .Select(async r =>
                    {
                        var body = await r.Content.ReadAsStringAsync();
                        return new { Response = r, Body = body };
                    })
                    .Select(task => task.Result)
                    .Subscribe(pair =>
                    {
                        ResponseViewerContext.ResponseBodyText = pair.Body;
                        ResponseViewerContext.ResponseHeadersText = pair.Response.Headers.ToString();
                    });
            }

            public override void Dispose()
            {
                responsesSubscription.Dispose();

                base.Dispose();
            }
        }
    }
}
