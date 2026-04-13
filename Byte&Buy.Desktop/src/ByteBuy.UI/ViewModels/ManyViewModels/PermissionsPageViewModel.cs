using ByteBuy.Core.Filtration.Permission;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Permission;
using ByteBuy.UI.ViewModels.Shared;
using ByteBuy.UI.ViewModels.SingleViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.ManyViewModels;

public partial class PermissionsPageViewModel : ViewModelMany<PermissionManyListItem, IPermissionService>
{
    #region Filtration Fields

    [ObservableProperty]
    private string? _name = null;

    [ObservableProperty]
    private string? _description = null;

    #endregion
    public PermissionsPageViewModel(AlertViewModel alert, INavigationService navigation, IDialogService dialogNavigation, IPermissionService service) 
        : base(alert, navigation, dialogNavigation, service)
    {
        PageName = ApplicationPageNames.Permissions;
    }

    public override async Task LoadDataAsync()
    {
        var query = new PermissionListQuery
        {
            Description = Description,
            Name = Name,
            PageNumber = PageNumber,
            PageSize = PageSize
        };

        var result = await Service.GetListAsync(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<PermissionManyListItem>(
            value.Items.Select((p, i) =>
                p.ToListItem(i + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    public override async Task ClearFiltersAsync()
    {
        Description = null;
        Name = null;
        await LoadDataAsync();
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Permission, async vm =>
        {
            if (vm is PermissionPageViewModel permVm)
                await permVm.InitializeForAddAsync();
        });
    }

    protected override async Task EditAsync(PermissionManyListItem item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Permission, async vm =>
        {
            if (vm is PermissionPageViewModel permVm)
                await permVm.InitializeForEditAsync(item.Id);
        });
    }
}
