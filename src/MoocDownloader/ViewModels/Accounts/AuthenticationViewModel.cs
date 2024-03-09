using Akavache;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Dialogs.Messages;
using MoocDownloader.Utilities.Browsers;
using MoocDownloader.ViewModels.Shared;
using MoocDownloader.Views.Accounts;
using MoocDownloader.Views.Dialogs;
using MoocResolver.Contracts;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace MoocDownloader.ViewModels.Accounts;

public partial class AuthenticationViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private WebsiteModel? _website;

    [ObservableProperty]
    private string _cookies = string.Empty;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    public AuthenticationViewModel(IContainer container) : base(container)
    {
    }

    public override void OnDialogOpened(IDialogParameters parameters)
    {
        Website = parameters.GetValue<WebsiteModel>(nameof(WebsiteModel));

        // Load authentication from the local database.
        BlobCache.LocalMachine
            .GetObject<Authentication>(Website.Name)
            .Subscribe(onNext: authentication =>
            {
                Cookies = authentication?.Cookies ?? string.Empty;
                Username = authentication?.Username ?? string.Empty;
                Password = authentication?.Password ?? string.Empty;
            });

        base.OnDialogOpened(parameters);
    }

    [RelayCommand]
    private async Task SavePasswordAsync()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            var messageOption = new MessageOption
            {
                Title = "提示",
                Message = "账号或密码不能为空。",
                ConfirmText = "好的",
                MessageType = MessageType.Warning
            };
            var dialogParameters = new DialogParameters
            {
                { nameof(MessageOption), messageOption }
            };

            DialogService.ShowDialog(
                name: nameof(MessageView),
                parameters: dialogParameters,
                callback: _ => { });

            return;
        }

        var authentication = new Authentication
        {
            AuthenticationType = AuthenticationType.Account,
            Username = Username,
            Password = Password,
        };

        // Save authentication to the local database.
        await BlobCache.LocalMachine
            .InsertObject(Website!.Name, authentication);

        Close(new DialogResult(result: ButtonResult.OK, parameters: new DialogParameters
        {
            { nameof(Authentication), authentication }
        }));
    }

    [RelayCommand]
    private async Task LaunchBrowserAsync()
    {
        var dialogParameters = new DialogParameters
        {
            { nameof(WebsiteModel), Website }
        };

        DialogService.ShowDialog(
            name: nameof(BrowserView),
            parameters: dialogParameters,
            callback: result =>
            {
                if (result.Result != ButtonResult.OK) return;

                var chromiumCookies = result.Parameters.GetValue<List<ChromiumCookie>>(nameof(ChromiumCookie));

                Cookies = SerializeCookies(chromiumCookies);
            });

        var authentication = new Authentication
        {
            AuthenticationType = AuthenticationType.Cookies,
            Cookies = Cookies,
        };

        await BlobCache.LocalMachine
            .InsertObject(Website!.Name, authentication);

        Close(new DialogResult(result: ButtonResult.OK, parameters: new DialogParameters
        {
            { nameof(Authentication), authentication }
        }));
    }

    [RelayCommand]
    private void ImportCookies(string browser)
    {
        // Support to import cookies from Edge & Chrome browser.
        try
        {
            var cookieDomains = Website!.CookieDomains.ToArray();
            var browserCookies = new List<ChromiumCookie>();
            switch (browser)
            {
                case "Edge": // Import cookies from Edge browser.
                    browserCookies = ChromiumUtility.ImportCookiesFromEdge(cookieDomains);
                    break;
                case "Chrome": // Import cookies from Chrome browser.
                    browserCookies = ChromiumUtility.ImportCookiesFromChrome(cookieDomains);
                    break;
            }

            if (browserCookies.Count == 0)
            {
                var messageOption = new MessageOption
                {
                    Title = "Cookie 读取失败",
                    Message = $"无法从 {browser} 浏览器中读取 Cookie 信息。读取 Cookies 之前确保网站 {Website.Name} 已经在 {browser} 浏览器中已经登录成功。",
                    CancelText = string.Empty,
                    ConfirmText = "确定",
                    MessageType = MessageType.Error
                };
                var messageDialogParameters = new DialogParameters
                {
                    { nameof(MessageOption), messageOption }
                };

                DialogService.ShowDialog(
                    name: nameof(MessageView),
                    parameters: messageDialogParameters,
                    callback: _ => { });
                return;
            }

            Cookies = SerializeCookies(browserCookies);
        }
        catch (Exception)
        {
            var messageOption = new MessageOption
            {
                Title = "Cookie 读取失败",
                Message = $"无法从 {browser} 浏览器中读取 Cookie 信息。从浏览器中读取 Cookie 信息之前应关闭浏览器，或正确安装支持的浏览器。",
                CancelText = string.Empty,
                ConfirmText = "确定",
                MessageType = MessageType.Error
            };
            var messageDialogParameters = new DialogParameters
            {
                { nameof(MessageOption), messageOption }
            };

            DialogService.ShowDialog(
                name: nameof(MessageView),
                parameters: messageDialogParameters,
                callback: _ => { });
        }
    }

    [RelayCommand]
    private async Task SaveCookiesAsync()
    {
        if (string.IsNullOrEmpty(Cookies))
        {
            var messageOption = new MessageOption
            {
                Title = "提示",
                Message = "Cookies 信息不能为空。",
                ConfirmText = "好的",
                MessageType = MessageType.Warning
            };
            var dialogParameters = new DialogParameters
            {
                { nameof(MessageOption), messageOption }
            };

            DialogService.ShowDialog(
                name: nameof(MessageView),
                parameters: dialogParameters,
                callback: _ => { });

            return;
        }

        var authentication = new Authentication
        {
            AuthenticationType = AuthenticationType.Cookies,
            Cookies = Cookies,
        };

        await BlobCache.LocalMachine
            .InsertObject(Website!.Name, authentication);

        Close(new DialogResult(result: ButtonResult.OK, parameters: new DialogParameters
        {
            { nameof(Authentication), authentication }
        }));
    }

    private static string SerializeCookies(List<ChromiumCookie> cookies)
    {
        return JsonSerializer.Serialize(cookies, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true,
        });
    }
}