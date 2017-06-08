using Restofus.Navigation;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restofus.Components
{
    public class Navigator
    {
        public event EventHandler<ReactiveFile> Navigating;

        public Task Navigate(ReactiveFile file)
        {
            Navigating?.Invoke(this, file);

            return Task.CompletedTask;
        }
    }

    public static class NavigatorExtensions
    {
        public static IObservable<ReactiveFile> GetNavigationObservable(this Navigator navigator)
        {
            return Observable.FromEventPattern<ReactiveFile>(
                        h => navigator.Navigating += h, h => navigator.Navigating -= h)
                    .Select(e => e.EventArgs);
        }
    }
}
