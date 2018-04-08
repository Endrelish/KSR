namespace KSR1.View
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    public class PercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int val)
            {
                return (100 - val).ToString();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string val)
            {
                int.TryParse(val, out var res);
                return 100 - res;
            }
            return null;
        }
    }
}