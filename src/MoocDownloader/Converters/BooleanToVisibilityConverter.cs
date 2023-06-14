using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MoocDownloader.Converters;

/// <summary>
/// This converter converts <see cref="bool"/> value to <see cref="System.Windows.Visibility"/>.
/// </summary>
public class BooleanToVisibilityConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool visibility)
        {
            return visibility ? Visibility.Visible : Visibility.Collapsed;
        }

        return Visibility.Visible;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility switch
            {
                Visibility.Visible => true,
                Visibility.Hidden => false,
                Visibility.Collapsed => false,
                _ => false
            };
        }

        return false;
    }
}