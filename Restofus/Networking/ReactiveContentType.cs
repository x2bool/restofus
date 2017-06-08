using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Networking
{
    public class ReactiveContentType : ReactiveObject
    {
        public ReactiveContentType(string mime)
        {
            this.mime = mime;
        }

        string mime;
        public string Name
        {
            get => mime;
            set => this.RaiseAndSetIfChanged(ref mime, value);
        }
    }
}
