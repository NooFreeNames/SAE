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
        public const string confirmed = "Подтверждено";
        public const string notConfirmed = "Не подтверждено";
        public object Convert(object value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {
            return (SAE_DB.StatusEnum)value == SAE_DB.StatusEnum.NotConfirmed ? notConfirmed : confirmed;
            
        }

        public object ConvertBack(object value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {
            if ((string)value == confirmed)
            {
                return SAE_DB.StatusEnum.Confirmed.ToString();
            }
            return SAE_DB.StatusEnum.NotConfirmed.ToString();
        }
    }
}
