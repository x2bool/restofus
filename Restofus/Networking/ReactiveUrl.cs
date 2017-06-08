using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Networking
{
    public class ReactiveUrl : ReactiveObject
    {
        public ReactiveUrl(string address)
        {
            this.address = address;
        }

        string address;
        public string Address
        {
            get => address;
            set => this.RaiseAndSetIfChanged(ref address, value);
        }

        public override string ToString()
        {
            return address;
        }
    }
}
