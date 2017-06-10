using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Utils
{
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type type);
        object Resolve(string name);
    }
}
