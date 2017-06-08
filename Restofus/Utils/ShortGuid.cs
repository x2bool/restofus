using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Utils
{
    public struct ShortGuid
    {
        readonly Guid guid;
        
        public static ShortGuid Empty { get; } = new ShortGuid(Guid.Empty);

        ShortGuid(Guid guid)
        {
            this.guid = guid;
        }
        
        public override string ToString()
        {
            return Convert.ToBase64String(guid.ToByteArray())
                .Substring(0, 22)
                .Replace("/", "_")
                .Replace("+", "-");
        }
        
        public static ShortGuid Parse(string encoded)
        {
            if (encoded == null)
            {
                throw new ArgumentNullException(nameof(encoded));
            }
            else if (encoded.Length != 22)
            {
                throw new FormatException("Incorrect string length");
            }

            var bytes = Convert.FromBase64String(encoded.Replace("_", "/").Replace("-", "+") + "==");

            return new ShortGuid(new Guid(bytes));
        }

        public static bool TryParse(string encoded, out ShortGuid guid)
        {
            try
            {
                guid = Parse(encoded);
                return true;
            }
            catch
            {
                guid = Empty;
                return false;
            }
        }

        public static explicit operator Guid(ShortGuid guid)
        {
            return guid.guid;
        }

        public static explicit operator ShortGuid(Guid guid)
        {
            return new ShortGuid(guid);
        }
    }
}
