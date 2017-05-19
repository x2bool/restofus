using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Restofus.Pads;

namespace Restofus
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.AttachDevTools();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context
        {
            public RequestPad.Context RequestPadContext { get; }

            public ResponsePad.Context ResponsePadContext { get; }

            public Context(
                RequestPad.Context requestPadContext,
                ResponsePad.Context responsePadContext)
            {
                RequestPadContext = requestPadContext;
                ResponsePadContext = responsePadContext;
            }
        }
    }
}
