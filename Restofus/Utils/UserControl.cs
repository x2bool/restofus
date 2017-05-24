using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Restofus.Utils
{
    public abstract class UserControl<T> : UserControl where T : class
    {
        protected T GetContext()
        {
            return DataContext as T;
        }

        protected void WithContext(Action<T> action)
        {
            var context = DataContext;

            if (context != null)
            {
                action((T)context);
            }
        }
    }
}
