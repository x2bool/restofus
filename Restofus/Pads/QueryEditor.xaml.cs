using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using Restofus.Views;

namespace Restofus.Pads
{
    public class QueryEditor : UserControl<QueryEditor.Context>
    {
        public QueryEditor()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : ReactiveObject
        {
            public KeyValueEditor.Model KeyValueEditorModel { get; }
        }
    }
}
