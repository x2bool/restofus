using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using Newtonsoft.Json;
using ReactiveUI;
using Restofus.Components;
using Restofus.Networking;
using Restofus.Utils;
using System;
using System.IO;
using System.Reactive.Linq;

namespace Restofus.Views
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
                    catch (Exception)
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
            IDisposable responseSubscription;
            IDisposable headersSubscription;
            IDisposable contentSubscription;
            IDisposable streamSubscription;

            public Context(
                IResolver resolver,
                RequestDispatcher httpDispatcher) : base(resolver)
            {
                responseSubscription = httpDispatcher
                    .GetResponseObservable()
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(ObserveResponse);
            }

            void ObserveResponse(ReactiveResponse response)
            {
                Response = response;

                contentSubscription?.Dispose();
                contentSubscription = response.WhenAnyValue(x => x.Content)
                    .Subscribe(ObserveContent);

                headersSubscription?.Dispose();
                headersSubscription = response.WhenAnyValue(x => x.Headers)
                    .Subscribe(ObserveHeaders);
            }

            void ObserveHeaders(ReactiveHeaderCollection headers)
            {
                var viewerContext = Get<HeadersViewer.Context>();
                viewerContext.Headers = headers;
            }

            void ObserveContent(ReactiveResponseContent content)
            {
                streamSubscription?.Dispose();
                streamSubscription = content.WhenAnyValue(x => x.MemoryStream)
                    .Where(s => s != null)
                    .Subscribe(ObserveStream);
            }

            void ObserveStream(MemoryStream stream)
            {
                StreamReader reader = new StreamReader(stream);
                ResponseBodyText = reader.ReadToEnd();
            }
            
            ReactiveResponse response;
            public ReactiveResponse Response
            {
                get => response;
                set => this.RaiseAndSetIfChanged(ref response, value);
            }

            string responseBodyText;
            public string ResponseBodyText
            {
                get => responseBodyText;
                set => this.RaiseAndSetIfChanged(ref responseBodyText, value);
            }
        }
    }
}
