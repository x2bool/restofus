using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restofus.Components
{
    public class Navigator
    {
        public event EventHandler<object> Navigating;

        public Task Navigate(object obj)
        {
            Navigating?.Invoke(this, obj);

            return Task.CompletedTask;
        }
    }

    public static class NavigatorExtensions
    {
        public static IObservable<object> GetNavigationObservable(this Navigator navigator)
        {
            return Observable.FromEventPattern<object>(
                        h => navigator.Navigating += h, h => navigator.Navigating -= h)
                    .Select(e => e.EventArgs);
        }
    }
}
