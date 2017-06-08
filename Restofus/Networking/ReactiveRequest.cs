using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Networking
{
    public class ReactiveRequest : ReactiveObject, IDisposable
    {
        ReactiveMethod method;
        public ReactiveMethod Method
        {
            get => method;
            set => this.RaiseAndSetIfChanged(ref method, value);
        }

        ReactiveUrl url;
        public ReactiveUrl Url
        {
            get => url;
            set => this.RaiseAndSetIfChanged(ref url, value);
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
            return $"{method}: {url}";
        }

        public ReactiveRequest Clone()
        {
            return new ReactiveRequest
            {
                method = method?.Clone(),
                url = url,
                content = content?.Clone()
            };
        }

        public void Dispose()
        {
            content?.Dispose();
        }
    }
}
