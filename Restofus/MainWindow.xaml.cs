using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
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

        public class Context : ReactiveObject
        {
            public NavigationPad.Context NavigationPadContext { get; }

            public RequestPad.Context RequestPadContext { get; }
            
            public ResponsePad.Context ResponsePadContext { get; }

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
