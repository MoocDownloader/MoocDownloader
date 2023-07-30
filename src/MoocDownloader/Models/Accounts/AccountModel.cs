using CommunityToolkit.Mvvm.ComponentModel;

namespace MoocDownloader.Models.Accounts;

public partial class AccountModel : ObservableObject
{
    [ObservableProperty]
    private AccountType _type = AccountType.None;

    [ObservableProperty]
    private AccountStatus _status = AccountStatus.None;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _cookieData = string.Empty;
}