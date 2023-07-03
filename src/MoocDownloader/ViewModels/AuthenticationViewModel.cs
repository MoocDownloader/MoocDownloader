using DryIoc;

namespace MoocDownloader.ViewModels;

public class AuthenticationViewModel : SharedDialogViewModel
{
    /// <inheritdoc />
    public AuthenticationViewModel(IContainer container) : base(container)
    {
    }
}