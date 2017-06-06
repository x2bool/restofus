using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Components.Http
{
    public class ReactiveRequest : ReactiveObject, IDisposable
    {
        ReactiveMethod method;
        public ReactiveMethod Method
        {
            get => method;
            set => this.RaiseAndSetIfChanged(ref method, value);
        }

        string address;
        public string Address
        {
            get => address;
            set => this.RaiseAndSetIfChanged(ref address, value);
        }

        ReactiveHeaderCollection headers;
        public ReactiveHeaderCollection Headers
        {
            get => headers;
            set => this.RaiseAndSetIfChanged(ref headers, value);
        }

        ReactiveRequestContent content;
        public ReactiveRequestContent Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public override string ToString()
        {
            return $"{method}: {address}";
        }

        public ReactiveRequest Clone()
        {
            return new ReactiveRequest
            {
                method = method?.Clone(),
                address = address,
                content = content?.Clone()
            };
        }

        public void Dispose()
        {
            content?.Dispose();
        }
    }
}
