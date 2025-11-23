using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class AccountPageViewModel : ViewModelBase
{
    #region Fields
    [ObservableProperty]
    private string _firstName = null!;
    
    [ObservableProperty]
    private string _lastName = null!;
    
    [ObservableProperty]   
    private string _email = null!;

    [ObservableProperty] 
    private string _street = null!;
    
    [ObservableProperty]
    private string _houseNumber = null!;
    
    [ObservableProperty]
    private string _postalCode = null!;
    
    [ObservableProperty]
    private string _city = null!;
    
    [ObservableProperty]
    private string _country = null!;

    [ObservableProperty]
    private string? _flatNumber;
    
    [ObservableProperty]
    private string _error = null!;
    
    [ObservableProperty]
    private Guid selectedRoleId = Guid.Empty;
    
    #endregion
    private readonly IEmployeeService _employeeService;

    public AccountPageViewModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService; 
        _ = LoadData();
    }

    [RelayCommand]
    private async Task LoadData()
    {
        var result = await _employeeService.GetSelf();
        if(!result.Success)
            Error = result.Error!.Description;
        
        var employeeResponse = result.Value;
        
        FirstName = employeeResponse!.FirstName;
        LastName = employeeResponse.LastName;
        City = employeeResponse.City;
        HouseNumber = employeeResponse.HouseNumber;
        PostalCode = employeeResponse.PostalCode;
        FlatNumber = employeeResponse.FlatNumber;
        Email =  employeeResponse.Email;
        Street = employeeResponse.Street;
    }
}