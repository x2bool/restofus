using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using AvaloniaEdit;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Indentation.CSharp;
using Newtonsoft.Json;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using System;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Restofus.Pads
{
    public class ResponseViewer : UserControl<ResponseViewer.Context>
    {
        TextEditor responseBodyEditor;
        IDisposable responseBodyTextSubscription;

        public ResponseViewer()
        {
            AvaloniaXamlLoader.Load(this);

            responseBodyEditor = this.FindControl<TextEditor>(nameof(responseBodyEditor));
            //responseBodyEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
            //responseBodyEditor.TextArea.IndentationStrategy = new CSharpIndentationStrategy();
            
            DataContextChanged += ContextChanged;
        }

        void ContextChanged(object sender, EventArgs e)
        {
            responseBodyTextSubscription?.Dispose();
            
            responseBodyTextSubscription = GetContext()?.WhenAnyValue(c => c.ResponseBodyText)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Where(text => text != null)
                .Subscribe(text =>
                {
                    try
                    {
                        responseBodyEditor.Text = FormatJson(text);
                    }
                    catch (Exception exception)
                    {
                        throw;
                    }
                });
        }

        string FormatJson(string text)
        {
            using (var stringReader = new StringReader(text))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }

        public class Context : BaseContext
        {
            string responseBodyText;
            public string ResponseBodyText
            {
                get => responseBodyText;
                set => this.RaiseAndSetIfChanged(ref responseBodyText, value);
            }

            string responseHeadersText;
            public string ResponseHeadersText
            {
                get => responseHeadersText;
                set => this.RaiseAndSetIfChanged(ref responseHeadersText, value);
            }
        }
    }
}
