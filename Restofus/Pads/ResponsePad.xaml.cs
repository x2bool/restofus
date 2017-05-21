using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using System.Reactive.Linq;

namespace Restofus.Pads
{
    public class ResponsePad : UserControl<ResponsePad.Context>
    {
        public ResponsePad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            public ResponseViewer.Context ResponseViewerContext { get; }

            public Context(
                ResponseViewer.Context responseViewerContext,
                I18N i18n,
                HttpDispatcher httpDispatcher)
            {
                ResponseViewerContext = responseViewerContext;

                httpDispatcher.Responses
                    .Select(r => r.Content.ReadAsStringAsync().Result)
                    .BindTo(ResponseViewerContext, x => x.ResponseBodyText);
            }
        }
    }
}
