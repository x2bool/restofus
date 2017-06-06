using Avalonia.Markup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Restofus.Converters
{
    public class SizeConverter : IValueConverter
    {
        public static SizeConverter Instance { get; } = new SizeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (value is int)
                {
                    return ConvertToReadable((int)value);
                }
                if (value is long)
                {
                    return ConvertToReadable((long)value);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        string ConvertToReadable(long val)
        {
            long absolute = (val < 0 ? -val : val);

            // determine the suffix and readable value
            string suffix;
            double readable;
            if (absolute >= 0x1000000000000000) // exabyte
            {
                suffix = "EB";
                readable = (val >> 50);
            }
            else if (absolute >= 0x4000000000000) // petabyte
            {
                suffix = "PB";
                readable = (val >> 40);
            }
            else if (absolute >= 0x10000000000) // terabyte
            {
                suffix = "TB";
                readable = (val >> 30);
            }
            else if (absolute >= 0x40000000) // gigabyte
            {
                suffix = "GB";
                readable = (val >> 20);
            }
            else if (absolute >= 0x100000) // megabyte
            {
                suffix = "MB";
                readable = (val >> 10);
            }
            else if (absolute >= 0x400) // kilobyte
            {
                suffix = "KB";
                readable = val;
            }
            else
            {
                return val.ToString("0 B"); // byte
            }
            // Divide by 1024 to get fractional value
            readable = (readable / 1024);
            // Return formatted number with suffix
            return readable.ToString("0.# ") + suffix;
        }
    }
}
