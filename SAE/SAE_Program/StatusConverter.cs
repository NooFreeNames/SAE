using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SAE_Program
{
    public class StatusConverter : IValueConverter
    {
        const string confirmed = "Подтверждено";
        const string notConfirmed = "Не подтверждено";
        public object Convert(object value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {
            return (sbyte)value == 0 ? notConfirmed : confirmed;
            
        }

        public object ConvertBack(object value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {
            if ((string)value == confirmed)
            {
                return 1;
            }
            return 0;
        }
    }
}
