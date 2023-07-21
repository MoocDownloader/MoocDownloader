using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MoocDownloader.Converters;

public class NumberToVisibilityConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return System.Convert.ToInt32(value) > 0
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility switch
            {
                Visibility.Visible => 1,
                Visibility.Hidden => 0,
                Visibility.Collapsed => 0,
                _ => 0
            };
        }

        return 0;
    }
}