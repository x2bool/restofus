using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Restofus.Components;
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

        public class Context : BaseContext
        {
            public NavigationPad.Context NavigationPadContext { get; set; }

            public RequestPad.Context RequestPadContext { get; set; }
            
            public ResponsePad.Context ResponsePadContext { get; set; }

            public Context(
                NavigationPad.Context navigationPadContext,
                RequestPad.Context requestPadContext,
                ResponsePad.Context responsePadContext)
            {
                NavigationPadContext = navigationPadContext;
                RequestPadContext = requestPadContext;
                ResponsePadContext = responsePadContext;
            }
        }
    }
}
