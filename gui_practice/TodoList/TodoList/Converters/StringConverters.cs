using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TodoList.Converters
{
    public static class StringConverters
    {
        public static readonly IValueConverter IsNotNullOrEmpty = new IsNotNullOrEmptyConverter();
    }

    public class IsNotNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is string str && !string.IsNullOrEmpty(str);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Not needed for one-way binding
            throw new NotImplementedException();
        }
    }
} 