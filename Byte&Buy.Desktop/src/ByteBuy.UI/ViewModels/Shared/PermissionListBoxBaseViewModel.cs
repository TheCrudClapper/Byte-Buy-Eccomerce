using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ModelsUI.Permission;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Shared;

public abstract partial class PermissionListBoxBaseViewModel : ObservableValidator
{
    #region Mvvm Fields
    [ObservableProperty] private ObservableCollection<PermissionListItemViewModel> _permissions = [];
    #endregion

    private readonly IPermissionService _permissionService;

    protected PermissionListBoxBaseViewModel(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public virtual async Task InitializeAsync()
    {
        var result = await _permissionService.GetSelectListAsync();
        if (!result.Success || result.Value is null)
            return;

        var list = result.Value.Select((p, index) => new PermissionListItemViewModel()
        {
            Id = p.Id,
            RowNumber = index + 1,
            IsSelected = false,
            Name = p.Title,
        });

        Permissions = new ObservableCollection<PermissionListItemViewModel>(list);
    }

    public virtual void ClearSelectedPermissions() { }
}