using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoocDownloader.Models.Credentials;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace MoocDownloader.ViewModels;

public partial class ServiceViewModel : ObservableRecipient, IDialogAware
{
    [ObservableProperty]
    private ObservableCollection<Service> _services = new();

    public ServiceViewModel()
    {
        Services.Add(new Service
        {
            Name = "哔哩哔哩 (゜-゜)つロ 干杯~",
            Url = "https://www.bilibili.com/",
            Image = new BitmapImage(new Uri(@"/Assets/Others/bilibili.png", UriKind.Relative)),
            SupportBrowser = true,
            SupportCookie = true,
        });
        Services.Add(new Service
        {
            Name = "中国大学MOOC(慕课)",
            Url = "https://www.icourse163.org/",
            Image = new BitmapImage(new Uri(@"/Assets/Others/icourse163.png", UriKind.Relative)),
            SupportBrowser = true,
            SupportCookie = true,
            SupportPassword = true,
        });
        Services.Add(new Service
        {
            Name = "爱课程",
            Url = "https://www.icourses.cn/home/",
            Image = new BitmapImage(new Uri(@"/Assets/Others/icourses.png", UriKind.Relative)),
            SupportBrowser = true,
            SupportCookie = true,
        });
        Services.Add(new Service
        {
            Name = "学堂在线",
            Url = "https://next.xuetangx.com/",
            Image = new BitmapImage(new Uri(@"/Assets/Others/xuetangx.png", UriKind.Relative)),
            SupportBrowser = true,
            SupportCookie = true,
        });
    }

    [RelayCommand]
    private void Close()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
    }

    /// <inheritdoc />
    public bool CanCloseDialog()
    {
        return true;
    }

    /// <inheritdoc />
    public void OnDialogClosed()
    {
    }

    /// <inheritdoc />
    public void OnDialogOpened(IDialogParameters parameters)
    {
    }

    /// <inheritdoc />
    public string Title { get; set; } = string.Empty;

    /// <inheritdoc />
    public event Action<IDialogResult>? RequestClose;
}