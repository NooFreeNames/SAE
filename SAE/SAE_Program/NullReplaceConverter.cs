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
    class NullReplaceConverter : IValueConverter
    {
        int dotIndex = -1;
        public object Convert(object value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {
            var num = ((IFormattable)value).ToString("", NumberFormatInfo.InvariantInfo);

            if (!num.Contains('.'))
            {
                if (dotIndex == -1)
                {
                    num += ".0";
                }
                else
                {
                    
                }
                
            }

            dotIndex = num.IndexOf('.');
            return num;
        }

        public object ConvertBack(object value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {

            return value;
        }
    }
}
