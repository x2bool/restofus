using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using Newtonsoft.Json;
using ReactiveUI;
using Restofus.Components;
using Restofus.Components.Http;
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

        public class Context : ReactiveObject
        {
            IDisposable responseSubscription;
            IDisposable contentSubscription;
            IDisposable streamSubscription;

            public Context(
                I18N i18n,
                RequestDispatcher httpDispatcher,
                HeadersViewer.Context headersViewerContext)
            {
                I18N = i18n;
                HeadersViewerContext = headersViewerContext;

                var requestObservable = Observable.FromEventPattern<ReactiveResponse>(
                        h => httpDispatcher.Response += h, h => httpDispatcher.Response -= h)
                    .Select(e => e.EventArgs);

                responseSubscription?.Dispose();
                responseSubscription = requestObservable
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(ObserveResponse);
            }

            void ObserveResponse(ReactiveResponse response)
            {
                Response = response;

                contentSubscription?.Dispose();
                contentSubscription = response.WhenAnyValue(x => x.Content)
                    .Subscribe(ObserveContent);
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

            public HeadersViewer.Context HeadersViewerContext { get; }

            public I18N I18N { get; }

            //string code;
            //public string Code
            //{
            //    get => code;
            //    set => this.RaiseAndSetIfChanged(ref code, value);
            //}

            //string time;
            //public string Time
            //{
            //    get => time;
            //    set => this.RaiseAndSetIfChanged(ref time, value);
            //}

            //string

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
