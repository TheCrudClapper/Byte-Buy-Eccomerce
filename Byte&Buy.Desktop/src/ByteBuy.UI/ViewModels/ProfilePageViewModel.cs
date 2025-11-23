using System;
using System.Threading.Tasks;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class ProfilePageViewModel : ViewModelBase
{
    #region Fields

    [ObservableProperty] private string _firstName = "Test";
    
    [ObservableProperty]
    private string _lastName = "Test";
    
    [ObservableProperty]   
    private string _email = "Test";

    [ObservableProperty] 
    private string _street = "Test";
    
    [ObservableProperty]
    private string _houseNumber = "Test";
    
    [ObservableProperty]
    private string _postalCode = "Test";
    
    [ObservableProperty]
    private string _city = "Test";
    
    [ObservableProperty]
    private string _country = "Test";

    [ObservableProperty]
    private string? _flatNumber = "Test";
    
    [ObservableProperty]
    private string _error = "Test";
    
    [ObservableProperty]
    private Guid _selectedRoleId = Guid.Empty;
    
    #endregion
    private readonly IEmployeeService _employeeService;

    public ProfilePageViewModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService; 
        _ = LoadData();
    }

    [RelayCommand]
    private async Task LoadData()
    {
        // var result = await _employeeService.GetSelf();
        // if(!result.Success)
        //     Error = result.Error!.Description;
        //
        // var employeeResponse = result.Value;
        //
        // FirstName = employeeResponse!.FirstName;
        // LastName = employeeResponse.LastName;
        // City = employeeResponse.City;
        // HouseNumber = employeeResponse.HouseNumber;
        // PostalCode = employeeResponse.PostalCode;
        // FlatNumber = employeeResponse.FlatNumber;
        // Email =  employeeResponse.Email;
        // Street = employeeResponse.Street;
    }
}