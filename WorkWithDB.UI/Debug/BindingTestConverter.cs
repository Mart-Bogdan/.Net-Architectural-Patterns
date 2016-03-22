using System;
using System.Globalization;
using System.Windows.Data;

namespace WorkWithDB.UI.Debug
{
    public class BindingTestConverter : IValueConverter
    {
        private static BindingTestConverter _instance;

        public static BindingTestConverter Instance
        {
            get { return _instance ?? (_instance = new BindingTestConverter()); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}