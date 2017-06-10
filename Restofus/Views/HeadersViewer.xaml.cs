using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using Restofus.Views;

namespace Restofus.Views
{
    public class HeadersViewer : UserControl<HeadersViewer.Context>
    {
        public HeadersViewer()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : ReactiveObject
        {
            public KeyValueEditor.Model KeyValueEditorModel { get; }
        }
    }
}
