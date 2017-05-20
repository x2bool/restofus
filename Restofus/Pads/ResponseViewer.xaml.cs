using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;

namespace Restofus.Pads
{
    public class ResponseViewer : UserControl
    {
        public ResponseViewer()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            string responseBodyText;
            public string ResponseBodyText
            {
                get => responseBodyText;
                set => this.RaiseAndSetIfChanged(ref responseBodyText, value);
            }

            string responseHeadersText;
            public string ResponseHeadersText
            {
                get => responseHeadersText;
                set => this.RaiseAndSetIfChanged(ref responseHeadersText, value);
            }
        }
    }
}
