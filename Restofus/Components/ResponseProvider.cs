using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Components
{
    public class ResponseProvider
    {
        Navigator navigator;
        Dispatcher dispatcher;
        
        public ResponseProvider(
            Navigator navigator,
            Dispatcher dispatcher)
        {
            this.navigator = navigator;
            this.dispatcher = dispatcher;
        }
    }
}
