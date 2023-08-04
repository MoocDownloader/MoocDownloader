using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MoocDownloader.Converters;

public class ObjectToVisibilityConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is null
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}