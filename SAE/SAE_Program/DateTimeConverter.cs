using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SAE_Program
{
    public class DateTimeConverter : IValueConverter
    {
        const string dateTimeFormat = "yyyy-MM-dd hh:mm:ss";
        public object? Convert(object value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {
            if (value == null) return null;
            return ((DateTime)value).ToString(dateTimeFormat);
            
        }

        public object? ConvertBack(object value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (value == null) return null;
            return DateTime.ParseExact((string)value, dateTimeFormat, provider);
        }
    }
}
