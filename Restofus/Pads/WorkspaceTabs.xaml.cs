using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System;
using System.Collections;
using ReactiveUI;
using System.Reactive;
using Restofus.Components;
using System.Threading.Tasks;
using System.Net.Http;
using Restofus.Utils;

namespace Restofus.Pads
{
    public class WorkspaceTabs : UserControl<WorkspaceTabs.Context>
    {
        public WorkspaceTabs()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            
        }
    }
    
}
