using DryIoc;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Preferences;

public class AboutViewModel : SharedDialogViewModel
{
    /// <inheritdoc />
    public AboutViewModel(IContainer container) : base(container)
    {
    }
}