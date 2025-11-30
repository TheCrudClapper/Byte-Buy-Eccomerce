using System.Threading.Tasks;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels;

public partial class RolePageViewModel : ViewModelSingle
{
    #region MVVM Fields
    [ObservableProperty]
    private string _roleName = string.Empty;
    #endregion
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

    private async Task InitializeForEdit()
    {
        
    }

    private void InitializeForAdd()
    {
        
    }

    protected override void Clear()
    {
        RoleName = string.Empty;
        PermissionListBox.SelectedPermission.Clear();
    }
}