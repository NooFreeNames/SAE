using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SAE_Program
{
    class EnumerableNullReplaceConverter : IValueConverter
    {
        public readonly string nullValue = App.Current.TryFindResource("NullStr").ToString() ?? "null";
        public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            
            return value;
        }

        public object? ConvertBack(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
