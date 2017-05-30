using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using Newtonsoft.Json;
using ReactiveUI;
using Restofus.Components;
using Restofus.Utils;
using System;
using System.IO;
using System.Reactive.Linq;

namespace Restofus.Pads
{
    public class ResponsePad : UserControl<ResponsePad.Context>
    {
        TextEditor responseBodyEditor;
        IDisposable responseBodyTextSubscription;

        public ResponsePad()
        {
            AvaloniaXamlLoader.Load(this);
            DataContextChanged += HandleDataContextChanged;
            
            responseBodyEditor = this.FindControl<TextEditor>(nameof(responseBodyEditor));
        }

        void HandleDataContextChanged(object sender, EventArgs e)
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

        public class Context : BaseContext, IDisposable
        {
            IDisposable responsesSubscription;

            public Context(
                I18N i18n,
                HttpDispatcher httpDispatcher)
            {
                responsesSubscription = httpDispatcher.Responses
                    .Select(async r =>
                    {
                        var body = await r.Content.ReadAsStringAsync();
                        return new { Response = r, Body = body };
                    })
                    .Select(task => task.Result)
                    .Subscribe(pair =>
                    {
                        ResponseBodyText = pair.Body;
                        ResponseHeadersText = pair.Response.Headers.ToString();
                    });
            }

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

            public override void Dispose()
            {
                responsesSubscription.Dispose();

                base.Dispose();
            }
        }
    }
}
