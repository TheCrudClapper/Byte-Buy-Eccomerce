using System.ComponentModel;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Employee;

namespace ByteBuy.UI.ViewModels;

public partial class ProfilePageViewModel : ViewModelBase
{
    #region Fields

    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _street = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(10)]
    private string _houseNumber = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(20)]
    private string _postalCode = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _city = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _country = string.Empty;

    [ObservableProperty] 
    [MaxLength(10)]
    private string? _flatNumber = string.Empty;
    
    [ObservableProperty]
    [DataType(DataType.PhoneNumber)]
    [MaxLength(15)]
    private string? _phoneNumber = string.Empty;

    [ObservableProperty]
    private string _error = string.Empty;
    #endregion

    private readonly IEmployeeService _employeeService;
    public AlertViewModel Alert { get; }
    public PasswordChangeViewModel PasswordComponent { get; }
    public ProfilePageViewModel(IEmployeeService employeeService,
        PasswordChangeViewModel passwordComponent,
        AlertViewModel alert)
    {
        _employeeService = employeeService;
        PasswordComponent  = passwordComponent;
        Alert = alert;
        _ = LoadData();
    }

    [RelayCommand]
    private void ClearFields()
    {
        Street = string.Empty;
        HouseNumber = string.Empty;
        FlatNumber = string.Empty;
        City = string.Empty;
        PhoneNumber = string.Empty;
        Country = string.Empty;
        PostalCode = string.Empty;
    }

    [RelayCommand]
    private async Task UpdateAddress()
    {
        ValidateAllProperties();
        if(HasErrors)
            return; 
        
        var request = new EmployeeAddressUpdateRequest(
            Street, HouseNumber, PostalCode, City, Country, FlatNumber, PhoneNumber
        );
        
        var result = await _employeeService.UpdateEmployeeAddress(request);

        if (!result.Success)
        {
            Error = result.Error!.Description; 
            await Alert.Show(AlertType.Error, Error);
        }
        else
        {
            await Alert.Show(AlertType.Success, "Address Updated Successfully");
        }
            
    }
    
    [RelayCommand]
    private async Task LoadData()
    {
        var result = await _employeeService.GetSelf();
        if (!result.Success)
            Error = result.Error!.Description;

        var employeeResponse = result.Value;

        FirstName = employeeResponse!.FirstName;
        LastName = employeeResponse.LastName;
        City = employeeResponse.City;
        HouseNumber = employeeResponse.HouseNumber;
        PostalCode = employeeResponse.PostalCode;
        FlatNumber = employeeResponse.FlatNumber;
        Country = employeeResponse.Country;
        Email = employeeResponse.Email;
        Street = employeeResponse.Street;
        PhoneNumber = employeeResponse.PhoneNumber;
    }
}