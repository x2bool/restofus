using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;

namespace Restofus.Views
{
    public class TabView : UserControl<TabView.Model>
    {
        public TabView()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Model
        {
            
        }
    }
}
