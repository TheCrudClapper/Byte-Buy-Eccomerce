using Avalonia.Controls;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.ManyViewModels;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.SingleViewModels
{
    public class AccessControlPageViewModel : PageViewModel
    {
        public RolesPageViewModel RolesPageViewModel { get; set; }
        public PermissionsPageViewModel PermissionPageViewModel { get; set; }
        private object? _selectedTab;
        public object? SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (SetProperty(ref _selectedTab, value))
                    _ = OnSelectedTabChangedAsync(value);
            }
        }

        public AccessControlPageViewModel(RolesPageViewModel rolesPageViewModel, PermissionsPageViewModel permissionPageViewModel)
        {
            PageName = ApplicationPageNames.AccessControl;
            PageBriefDescription = "Here you can define granular access control to our services.";
            RolesPageViewModel = rolesPageViewModel;
            PermissionPageViewModel = permissionPageViewModel;
        }

        private async Task OnSelectedTabChangedAsync(object? tab)
        {
            if (tab is TabItem tabItem)
                tab = tabItem.Content;

            switch (tab)
            {
                case RolesPageViewModel roles:
                    await roles.LoadDataAsync();
                    break;
                case PermissionsPageViewModel permissions:
                    await permissions.LoadDataAsync();
                    break;
            }
        }
    }
}
