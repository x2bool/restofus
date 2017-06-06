using Avalonia.Markup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Restofus.Converters
{
    public class StatusCodeConverter : IValueConverter
    {
        public static StatusCodeConverter Instance { get; } = new StatusCodeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (value is int)
                {
                    return ConvertToReadable((int)value);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        string ConvertToReadable(int code)
        {
            switch (code)
            {
                case 200:
                    return "200 OK";
                case 404:
                    return "404 Not Found";
            }

            return code.ToString();
        }
    }
}
