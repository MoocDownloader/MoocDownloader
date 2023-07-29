using DryIoc;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Preferences;

public class PreferenceViewModel : SharedDialogViewModel
{
    /// <inheritdoc />
    public PreferenceViewModel(IContainer container) : base(container)
    {
    }
}