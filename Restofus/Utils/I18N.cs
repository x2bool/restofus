using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Utils
{
    public class I18N
    {
        Dictionary<string, string> dict;

        public I18N()
        {
            dict = new Dictionary<string, string>
            {
                ["Send"] = "Send",
                ["Body"] = "Body",
                ["Headers"] = "Headers",
                ["Navigation"] = "Navigation",
                ["Response"] = "Response"
            };
        }

        public string this[string index]
        {
            get => dict[index];
        }
    }
}
