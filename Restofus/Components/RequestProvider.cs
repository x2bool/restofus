using Restofus.Navigation;
using Restofus.Networking;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Text;

namespace Restofus.Components
{
    public class RequestProvider
    {
        Navigator navigator;
        RequestSerializer serializer;

        event EventHandler<ReactiveRequest> RequestReady;
        public IObservable<ReactiveRequest> WhenRequestReady() =>
            this.FromEvent<ReactiveRequest>(h => RequestReady += h, h => RequestReady -= h);

        public RequestProvider(
            RequestSerializer serializer,
            Navigator navigator)
        {
            this.serializer = serializer;
            this.navigator = navigator;

            navigator.WhenSelected()
                .Where(f => f != null && !f.IsDirectory)
                .Where(f => f.Path.EndsWith(".req"))
                .Subscribe(ObserveSelection);
        }

        void ObserveSelection(ReactiveFile file)
        {
            serializer.Deserialize(new FileInfo(file.Path))
                .ContinueWith(task =>
                {
                    RequestReady?.Invoke(this, task.Result);
                });
        }
    }
}
