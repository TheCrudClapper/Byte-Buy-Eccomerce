using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel : PageViewModel
{
    #region Fields

    [ObservableProperty] 
    private int _itemsCount;

    [ObservableProperty]
    private ObservableCollection<EmployeeResponse> _items = [];
    #endregion
    
    private readonly MainWindowViewModel _main;
    private readonly IEmployeeService _employeeService;
    private readonly PageFactory _pageFactory;

    public EmployeesPageViewModel(
        MainWindowViewModel main,
        IEmployeeService employeeService,
        PageFactory pageFactory,
        AlertViewModel alert) : base(alert) 
    {
        PageName = ApplicationPageNames.Employees;
        _employeeService = employeeService;
        _pageFactory = pageFactory;
        _main = main;
        _ = LoadItems();
    }


    partial void OnItemsChanged(ObservableCollection<EmployeeResponse> value)
    {
        ItemsCount = Items.Count;
    }

    private async Task LoadItems()
    {
        var result = await _employeeService.GetAll();
        if (!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);

        Items = new ObservableCollection<EmployeeResponse>(result.Value!);
    }
    
    
    [RelayCommand]
    private void OpenEmployeePage()
        => _main.CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Employee);

}