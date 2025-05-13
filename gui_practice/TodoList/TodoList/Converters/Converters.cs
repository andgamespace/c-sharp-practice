using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using TodoList.Models;

namespace TodoList.Converters
{
    /// <summary>
    /// Converter to apply strikethrough text decoration to completed tasks
    /// </summary>
    public class BoolToStrikethroughConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isCompleted && isCompleted)
            {
                return TextDecorations.Strikethrough;
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converter to apply color based on task priority
    /// </summary>
    public class PriorityToBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Priority priority)
            {
                return priority switch
                {
                    Priority.Low => new SolidColorBrush(Color.Parse("#27ae60")),    // Green
                    Priority.Normal => new SolidColorBrush(Color.Parse("#3498db")), // Blue
                    Priority.High => new SolidColorBrush(Color.Parse("#e74c3c")),   // Red
                    _ => new SolidColorBrush(Color.Parse("#95a5a6"))               // Gray default
                };
            }
            return new SolidColorBrush(Color.Parse("#95a5a6"));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}