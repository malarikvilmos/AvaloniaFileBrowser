using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace FileBrowserApp.Converters
{
    public class SizeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString() ?? "<DIR>";

        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
