using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Networking;
using Restofus.Utils;
using Restofus.Views;

namespace Restofus.Views
{
    public class HeadersEditor : UserControl<HeadersEditor.Context>
    {
        public HeadersEditor()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            public Context(IResolver resolver) : base(resolver)
            {
                keyValueEditorModel = new KeyValueEditor.Model();

                this.WhenAnyValue(x => x.Headers)
                    .Subscribe(ObserveHeaders);
            }

            void ObserveHeaders(ReactiveHeaderCollection headers)
            {
                keyValueEditorModel.Items.Clear();

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        keyValueEditorModel.Items.Add(new KeyValueEditor.Item
                        {
                            Key = header.Name,
                            Value = header.Value
                        });
                    }
                }
            }

            ReactiveHeaderCollection headers;
            public ReactiveHeaderCollection Headers
            {
                get => headers;
                set => this.RaiseAndSetIfChanged(ref headers, value);
            }

            KeyValueEditor.Model keyValueEditorModel;
            public KeyValueEditor.Model KeyValueEditorModel
            {
                get => keyValueEditorModel;
                set => this.RaiseAndSetIfChanged(ref keyValueEditorModel, value);
            }
        }
    }
}
