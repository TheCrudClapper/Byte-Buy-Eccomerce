using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels;

public class SettingsPageViewModel : PageViewModel
{
    public SettingsPageViewModel(AlertViewModel alert) : base(alert)
    {
        PageName = ApplicationPageNames.Settings;
    }
}