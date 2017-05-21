using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Utils
{
    public class UserControl<T> : UserControl where T : class
    {
        public void WithContext(Action<T> action)
        {
            var context = DataContext;

            if (context != null)
            {
                action((T)context);
            }
        }
    }
}
