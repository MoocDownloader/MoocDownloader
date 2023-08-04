using CommunityToolkit.Mvvm.Messaging.Messages;
using MoocDownloader.Models.Downloads;

namespace MoocDownloader.Messages;

public class LibrarySelectedMessage : ValueChangedMessage<LibraryModel?>
{
    /// <inheritdoc />
    public LibrarySelectedMessage(LibraryModel? value) : base(value)
    {
    }
}