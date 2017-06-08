using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Restofus.Networking
{
    public class ReactiveResponseContent : ReactiveObject, IDisposable
    {
        MemoryStream memoryStream;
        public MemoryStream MemoryStream
        {
            get => memoryStream;
            set => this.RaiseAndSetIfChanged(ref memoryStream, value);
        }

        public void Dispose()
        {
            memoryStream?.Dispose();
        }
    }
}
