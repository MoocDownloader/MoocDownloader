using CommunityToolkit.Mvvm.ComponentModel;

namespace MoocDownloader.Models.Dialogs.Messages;

public partial class MessageOption : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private string _cancelText = string.Empty;

    [ObservableProperty]
    private string _confirmText = string.Empty;

    [ObservableProperty]
    private MessageType _messageType = MessageType.Info;
}