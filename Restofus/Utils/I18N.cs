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
                ["Query"] = "Query",
                ["Headers"] = "Headers",
                ["Navigation"] = "Navigation",
                ["Response"] = "Response",
                ["BaseAddress"] = "Base address",
                ["Parameters"] = "Parameters",
                ["Add"] = "Add"
            };
        }

        public string this[string index]
        {
            get
            {
                string item;
                dict.TryGetValue(index, out item);
                return item ?? "";
            }
        }
    }
}
