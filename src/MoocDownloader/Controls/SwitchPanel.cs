using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MoocDownloader.Controls;

public class SwitchPanel : ContentControl
{
    [Category("Appearance")]
    [TypeConverter(typeof(NullableBoolConverter))]
    public bool? IsChecked
    {
        get => (bool?)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public object CheckedContent
    {
        get => GetValue(CheckedContentProperty);
        set => SetValue(CheckedContentProperty, value);
    }

    public object UncheckedContent
    {
        get => GetValue(UncheckedContentProperty);
        set => SetValue(UncheckedContentProperty, value);
    }


    public object IndeterminateContent
    {
        get => GetValue(IndeterminateContentProperty);
        set => SetValue(IndeterminateContentProperty, value);
    }

    public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
        nameof(IsChecked),
        typeof(bool?), typeof(SwitchPanel),
        new PropertyMetadata(false));

    public static readonly DependencyProperty CheckedContentProperty = DependencyProperty.Register(
        nameof(CheckedContent),
        typeof(object),
        typeof(SwitchPanel),
        new PropertyMetadata(default));

    public static readonly DependencyProperty UncheckedContentProperty = DependencyProperty.Register(
        nameof(UncheckedContent),
        typeof(object),
        typeof(SwitchPanel),
        new PropertyMetadata(default));

    public static readonly DependencyProperty IndeterminateContentProperty = DependencyProperty.Register(
        nameof(IndeterminateContent),
        typeof(object),
        typeof(SwitchPanel),
        new PropertyMetadata(default));
}