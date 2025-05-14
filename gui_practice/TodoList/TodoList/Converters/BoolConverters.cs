using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TodoList.Converters
{
    public static class BoolConverters
    {
        public static readonly IValueConverter BoolToSwitchableString = new BoolToSwitchableStringConverter();
    }

    public class BoolToSwitchableStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue && parameter is string str)
            {
                string[] parts = str.Split('|');
                if (parts.Length == 2)
                {
                    return boolValue ? parts[0] : parts[1];
                }
            }
            return value?.ToString() ?? string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}