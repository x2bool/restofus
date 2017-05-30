using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using Restofus.Views;

namespace Restofus.Pads
{
    public class HeadersEditor : UserControl<HeadersEditor.Context>
    {
        public HeadersEditor()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            public KeyValueEditor.Model KeyValueEditorModel { get; }
        }
    }
}
