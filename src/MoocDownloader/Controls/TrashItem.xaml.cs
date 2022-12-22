using MoocDownloader.Models;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for TrashItem.xaml
/// </summary>
public partial class TrashItem
{
    public Course Course
    {
        get => (Course)GetValue(CourseProperty);
        set => SetValue(CourseProperty, value);
    }

    public static readonly DependencyProperty CourseProperty =
        DependencyProperty.Register(nameof(Course), typeof(Course), typeof(TrashItem));

    public TrashItem()
    {
        InitializeComponent();
    }
}