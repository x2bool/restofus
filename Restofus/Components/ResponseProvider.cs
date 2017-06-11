using Restofus.Networking;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Components
{
    public class ResponseProvider
    {
        Navigator navigator;
        Dispatcher dispatcher;

        event EventHandler<ReactiveResponse> ResponseReady;
        public IObservable<ReactiveResponse> WhenResponseReady() =>
            this.FromEvent<ReactiveResponse>(h => ResponseReady += h, h => ResponseReady -= h);
        
        public ResponseProvider(
            Navigator navigator,
            Dispatcher dispatcher)
        {
            this.navigator = navigator;
            this.dispatcher = dispatcher;

            dispatcher.WhenReceiving()
                .Subscribe(ObserveReceiving);
        }

        void ObserveReceiving(ReactiveResponse response)
        {
            ResponseReady?.Invoke(this, response);
        }
    }
}
