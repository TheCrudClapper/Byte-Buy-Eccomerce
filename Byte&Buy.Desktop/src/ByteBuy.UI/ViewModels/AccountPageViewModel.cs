using System;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class AccountPageViewModel : ViewModelBase
{
    #region Fields
    [ObservableProperty]
    private string _firstName;
    
    [ObservableProperty]
    private string _lastName;
    
    [ObservableProperty]   
    private string _email;

    [ObservableProperty] 
    private string _street;
    
    [ObservableProperty]
    private string _houseNumber;
    
    [ObservableProperty]
    private string _postalCode;
    
    [ObservableProperty]
    private string _city;
    
    [ObservableProperty]
    private string _country;

    [ObservableProperty]
    private string? _flatNumber;
    
    [ObservableProperty]
    private string _error;
    
    #endregion
    private readonly IEmployeeService _employeeService;

    public AccountPageViewModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [RelayCommand]
    private async void Get()
    {
        var result = await _employeeService.GetSelf();
        if(!result.Success)
            Error = result.Error!.Description;
        
    }
}