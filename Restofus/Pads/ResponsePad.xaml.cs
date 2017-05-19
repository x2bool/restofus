using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using System.Reactive.Linq;

namespace Restofus.Pads
{
    public class ResponsePad : UserControl
    {
        public ResponsePad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : ReactiveObject
        {
            public Context(
                HttpDispatcher httpDispatcher)
            {
                httpDispatcher.Responses
                    .Select(r => r.Content.ReadAsStringAsync().Result)
                    .BindTo(this, x => x.ResponseBody);
            }

            string responseBody;
            public string ResponseBody
            {
                get => responseBody;
                set => this.RaiseAndSetIfChanged(ref responseBody, value);
            }
        }
    }
}
