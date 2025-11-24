using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;

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

    [Required]
    [ObservableProperty]
    private string _street = string.Empty;

    [ObservableProperty] private string _houseNumber = string.Empty;

    [ObservableProperty] private string _postalCode = string.Empty;

    [ObservableProperty] private string _city = string.Empty;

    [ObservableProperty] private string _country = string.Empty;

    [ObservableProperty] private string? _flatNumber = string.Empty;

    [ObservableProperty] private string? _phoneNumber = string.Empty;

    [ObservableProperty]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    private string? _currentPassword = string.Empty;

    [ObservableProperty]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    private string? _confirmPassword = string.Empty;

    [ObservableProperty]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    private string? _newPassword = string.Empty;

    [ObservableProperty] private string _error = string.Empty;

    [ObservableProperty] private Guid _selectedRoleId = Guid.Empty;
    #endregion

    private readonly IEmployeeService _employeeService;

    public ProfilePageViewModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
        _ = LoadData();
    }

    [RelayCommand]
    private async Task ChangePassword()
    {
        throw new NotImplementedException();
        // var request = new PasswordChangeRequest(NewPassword, CurrentPassword, ConfirmPassword);
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
        Email = employeeResponse.Email;
        Street = employeeResponse.Street;
    }
}