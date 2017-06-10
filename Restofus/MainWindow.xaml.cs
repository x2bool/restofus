using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using Restofus.Views;

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
            public Context(IResolver resolver) : base (resolver)
            {

            }
        }
    }
}
