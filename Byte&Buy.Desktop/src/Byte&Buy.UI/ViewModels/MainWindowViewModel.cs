using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase _currentPage;
        
        private readonly DashboardPageViewModel _dashboardPage;
        private readonly EmployeesPageViewModel _employeesPage;

        public MainWindowViewModel(DashboardPageViewModel dashboardPage,
            EmployeesPageViewModel employeesPage)
        {
            _dashboardPage = dashboardPage;
            _employeesPage = employeesPage;
            _currentPage = _dashboardPage;
        }

        [RelayCommand]
        private void GoToDashboard() => CurrentPage = _dashboardPage;
        
        [RelayCommand]
        private void GoToEmployees() => CurrentPage = _employeesPage;
    }
}
