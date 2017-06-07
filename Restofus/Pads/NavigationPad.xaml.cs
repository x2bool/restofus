using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using System.Threading.Tasks;

namespace Restofus.Pads
{
    public class NavigationPad : UserControl<NavigationPad.Context>
    {
        public NavigationPad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : ReactiveObject
        {
            public Context(
                Navigator navigator)
            {
                Task.Run(async () => {
                    await Task.Delay(5000);
                    await navigator.Navigate(null);
                });
            }
        }
    }
}
