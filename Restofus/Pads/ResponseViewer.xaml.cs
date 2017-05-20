using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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

        }
    }
}
