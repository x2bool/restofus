using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;

namespace Restofus.Pads
{
    public class StatusBar : UserControl<StatusBar.Context>
    {
        public StatusBar()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            
        }
    }
}
