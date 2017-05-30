using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using System.Collections.Generic;

namespace Restofus.Views
{
    public class KeyValueEditor : UserControl<KeyValueEditor.Model>
    {
        public KeyValueEditor()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Model
        {
            public Items Items { get; } = new Items();
        }

        public class Item : ReactiveObject
        {
            string key;
            public string Key
            {
                get => key;
                set => this.RaiseAndSetIfChanged(ref key, value);
            }

            string val;
            public string Value
            {
                get => val;
                set => this.RaiseAndSetIfChanged(ref val, value);
            }

            bool isButton = false;
            public bool IsButton
            {
                get => isButton;
                set
                {
                    this.RaiseAndSetIfChanged(ref isButton, value);
                    this.RaiseAndSetIfChanged(ref isItem, !value);
                }
            }

            bool isItem = true;
            public bool IsItem
            {
                get => isItem;
                set
                {
                    this.RaiseAndSetIfChanged(ref isItem, value);
                    this.RaiseAndSetIfChanged(ref isButton, !value);
                }
            }
        }

        public class Items : ReactiveList<Item>
        {

        }
    }
}
