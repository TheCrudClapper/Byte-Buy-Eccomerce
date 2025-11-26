using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels;

public partial class RolePageViewModel : PageViewModel
{
    public RolePageViewModel(AlertViewModel alert) : base(alert)
    {
        PageName = ApplicationPageNames.Role;
    }
}