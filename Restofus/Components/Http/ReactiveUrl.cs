using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Components.Http
{
    public class ReactiveUrl : ReactiveObject
    {
        string address;
        public string Address
        {
            get => address;
            set => this.RaiseAndSetIfChanged(ref address, value);
        }
    }
}
