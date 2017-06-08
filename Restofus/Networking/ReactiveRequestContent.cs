using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Restofus.Networking
{
    public class ReactiveRequestContent : ReactiveObject, IDisposable
    {
        MemoryStream memoryStream;
        public MemoryStream MemoryStream
        {
            get => memoryStream;
            set => this.RaiseAndSetIfChanged(ref memoryStream, value);
        }

        public ReactiveRequestContent Clone()
        {
            return new ReactiveRequestContent
            {
                memoryStream = memoryStream
            };
        }

        public void Dispose()
        {
            memoryStream?.Dispose();
        }
    }
}
