using Humanizer;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MoocDownloader.Converters;

/// <summary>
/// The converter for formatting file size.
/// </summary>
public class FormatSizeConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is long size)
        {
            return size.Bytes().Humanize();
        }

        return 0.Bytes().Humanize();
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 0;
    }
}