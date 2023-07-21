using MoocDownloader.Models.Credentials;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for CredentialItem.xaml
/// </summary>
public partial class CredentialItem
{
    public Credential Credential
    {
        get => (Credential)GetValue(CredentialProperty);
        set => SetValue(CredentialProperty, value);
    }

    public static readonly DependencyProperty CredentialProperty =
        DependencyProperty.Register(nameof(Credential), typeof(Credential), typeof(CredentialItem));

    public CredentialItem()
    {
        InitializeComponent();
    }
}