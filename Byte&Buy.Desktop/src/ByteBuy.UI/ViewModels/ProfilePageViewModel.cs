using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class ProfilePageViewModel : PageViewModel
{
    #region Fields

    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _roleName = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _street = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _postalCity = string.Empty;

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
    private string _phoneNumber = string.Empty;
    #endregion

    private readonly IEmployeeService _employeeService;
    public PasswordChangeViewModel PasswordComponent { get; }
    public ProfilePageViewModel(IEmployeeService employeeService,
        PasswordChangeViewModel passwordComponent,
        AlertViewModel alert) : base(alert)
    {
        _employeeService = employeeService;
        PasswordComponent = passwordComponent;
    }

    [RelayCommand]
    private async Task Save()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        var address = new HomeAddressDto()
        {
            PostalCity = PostalCity,
            PostalCode = PostalCode,
            HouseNumber = HouseNumber,
            City = City,
            Street = Street,
            Country = Country,
            FlatNumber = FlatNumber        
        };
        var request = new EmployeeAddressUpdateRequest(
            address, PhoneNumber
        );

        var result = await _employeeService.UpdateAddress(request);
        HandleResult(result, "Address updated successfully !");
    }

    [RelayCommand]
    private void Clear()
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
    public async Task LoadData()
    {
        var result = await _employeeService.GetSelf();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        ProfilePageMappings.MapFromResponse(this, value);
    }
}