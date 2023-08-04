using System;
using System.Globalization;
using System.Windows.Data;

namespace MoocDownloader.Converters;

public class ObjectToBooleanConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is not null;
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}