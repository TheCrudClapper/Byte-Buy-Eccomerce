using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.PortalUser;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class PortalUsersPageViewModel : ViewModelMany<PortalUserListItemViewModel, IPortalUserService>
{
    #region Filtration fields

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _lastName;

    [ObservableProperty]
    private string? _email;

    public PortalUsersPageViewModel(
        AlertViewModel alert,
        INavigationService navigation,
        IDialogService dialogNavigation,
        IPortalUserService userService) : base(alert, navigation, dialogNavigation, userService)
    {
        PageName = ApplicationPageNames.PortalUsers;
    }
    #endregion

    protected override async Task EditAsync(PortalUserListItemViewModel item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.PortalUser, async vm =>
        {
            if (vm is PortalUserPageViewModel userVm)
                await userVm.InitializeForEdit(item.Id);
        });
    }

    public override async Task LoadDataAsync()
    {
        var query = new PortalUserListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
        };

        var result = await Service.GetListAsync(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<PortalUserListItemViewModel>(
            value.Items.Select((u, index) =>
                u.ToListItem(index + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.PortalUser, async vm =>
        {
            if (vm is PortalUserPageViewModel userVm)
                await userVm.InitializeForAddAsync();
        });
    }

    public override async Task ClearFiltersAsync()
    {
        FirstName = null;
        LastName = null;
        Email = null;
        await LoadDataAsync();
    }
}
