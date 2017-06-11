using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace Restofus.Utils
{
    public static class ObservableExtensions
    {
        public static IObservable<TArg> FromEvent<TArg>(
            this object _,
            Action<EventHandler<TArg>> addHandler,
            Action<EventHandler<TArg>> removeHandler)
        {
            return Observable.FromEventPattern(addHandler, removeHandler)
                    .Select(e => e.EventArgs);
        }
    }
}
