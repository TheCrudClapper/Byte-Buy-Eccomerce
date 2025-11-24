using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Permission;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeePageViewModel : ViewModelBase
{
    #region Fields
    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _phoneNumber = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _street = string.Empty;

    [ObservableProperty]
    private string _houseNumber = string.Empty;

    [ObservableProperty]
    private string _postalCode = string.Empty;

    [ObservableProperty]
    private string _city = string.Empty;

    [ObservableProperty]
    private string _country = string.Empty;

    [ObservableProperty]
    private string? _flatNumber;

    [ObservableProperty]
    private string _error = string.Empty;

    [ObservableProperty]
    private Guid _selectedRoleId = Guid.Empty;

    #endregion

    private readonly IEmployeeService _employeeService;
    private readonly IPermissionService _permissionService;
    private ObservableCollection<PermissionItem> Permissions { get; set; } = [];

    public EmployeePageViewModel(IEmployeeService employeeService,
        IPermissionService permissionService)
    {
        _employeeService = employeeService;
        _permissionService = permissionService;
        _ = LoadData();
        _ = LoadPermissions();
    }


    [RelayCommand]
    private async Task LoadData()
    {
        //var result = await _employeeService.GetSelf();
        //if (!result.Success)
        //    Error = result.Error!.Description;

        //var employeeResponse = result.Value;

        //FirstName = employeeResponse!.FirstName;
        //LastName = employeeResponse.LastName;
        //City = employeeResponse.City;
        //HouseNumber = employeeResponse.HouseNumber;
        //PostalCode = employeeResponse.PostalCode;
        //FlatNumber = employeeResponse.FlatNumber;
        //Email = employeeResponse.Email;
        //Street = employeeResponse.Street;
    }

    [RelayCommand]
    private async Task LoadPermissions()
    {
        var result = await _permissionService.GetSelectList();
        var permissionItems = result.Value!
            .Select(p => new PermissionItem(p.Id, p.Title, false))
            .ToList();

        permissionItems.ForEach(Permissions.Add);
    }
}