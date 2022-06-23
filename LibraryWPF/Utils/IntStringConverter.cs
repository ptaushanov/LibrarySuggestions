using System;
using System.Windows.Data;

namespace LibraryWPF.Utils
{
    [ValueConversion(typeof(Int32), typeof(String))]
    public class IntStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int intValue = ((int)value);
            return intValue == default ? "" : intValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int.TryParse(value as string, out int result);
            return result;
        }
    }
}
