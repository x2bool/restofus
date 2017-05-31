using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Components.Http
{
    public class ReactiveMethod : ReactiveObject
    {
        public ReactiveMethod(string name)
        {
            this.name = name;
        }

        string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public override int GetHashCode()
        {
            return name?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ReactiveMethod;

            if (other == null)
            {
                return false;
            }

            return name == other.name;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
