using System.Threading.Tasks;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels;

public partial class RolePageViewModel : ViewModelSingle
{
    public PermissionListBoxViewModel PermissionListBox { get; }
    public RolePageViewModel(AlertViewModel alert,
        PermissionListBoxViewModel permissionListBox) : base(alert)
    {
        PageName = ApplicationPageNames.Role;
        PermissionListBox = permissionListBox;
    }

    protected override Task Save()
    {
        throw new System.NotImplementedException();
    }

    protected override void Clear()
    {
        throw new System.NotImplementedException();
    }
}