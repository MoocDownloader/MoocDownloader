using DryIoc;

namespace MoocDownloader.ViewModels;

public class PreferenceViewModel : SharedDialogViewModel
{
    /// <inheritdoc />
    public PreferenceViewModel(IContainer container) : base(container)
    {
    }
}