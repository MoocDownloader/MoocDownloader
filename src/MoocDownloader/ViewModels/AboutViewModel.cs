using DryIoc;

namespace MoocDownloader.ViewModels;

public class AboutViewModel : SharedDialogViewModel
{
    /// <inheritdoc />
    public AboutViewModel(IContainer container) : base(container)
    {
    }
}