using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Networking
{
    public class ReactiveHeader : ReactiveObject
    {
        public ReactiveHeader(string name, string val)
        {
            this.name = name;
            this.val = val;
        }

        string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        string val;
        public string Value
        {
            get => val;
            set => this.RaiseAndSetIfChanged(ref val, value);
        }

        public override string ToString()
        {
            return $"{name}: {val}";
        }

        public ReactiveHeader Clone()
        {
            return new ReactiveHeader(name, val);
        }
    }
}
