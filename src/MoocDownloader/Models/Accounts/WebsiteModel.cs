using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace MoocDownloader.Models.Accounts;

public partial class WebsiteModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private ImageSource? _avatar;

    [ObservableProperty]
    private string _loginUrl = string.Empty;

    [ObservableProperty]
    private string _url = string.Empty;

    [ObservableProperty]
    private string _matchPattern = string.Empty;

    [ObservableProperty]
    private bool _supportCookie;

    [ObservableProperty]
    private bool _supportPassword;

    [ObservableProperty]
    private List<string> _cookieDomains = new();

    [ObservableProperty]
    private List<string> _cookieNames = new();

    [ObservableProperty]
    private AccountModel _account = new();

    [ObservableProperty]
    private Type? _resolver;
}