using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public sealed partial class EmployeePageViewModel : ViewModelSingle
{
    #region MVVM Fields

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _lastName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [EmailAddress]
    private string _email = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    private string _street = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    private string _postalCity = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(10)]
    private string _houseNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(20)]
    private string _postalCode = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    private string _city = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MaxLength(10)]
    private string? _flatNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [Phone]
    [MaxLength(15)]
    private string _phoneNumber = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<Guid>> _countries = [];

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Choose country")]
    private SelectListItemResponse<Guid>? _selectedCountry;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Choose employee's roles")]
    private SelectListItemResponse<Guid>? _selectedRole;

    [ObservableProperty] private ObservableCollection<SelectListItemResponse<Guid>> _roles = [];

    #endregion

    private readonly IEmployeeService _employeeService;
    private readonly IRoleService _roleService;
    private readonly ICountryService _countryService;
    private readonly INotificationService _notificationService;

    public PermissionGrantRevokeViewModel PermissionListBox { get; }

    public EmployeePageViewModel(
        IRoleService roleService,
        IEmployeeService employeeService,
        PermissionGrantRevokeViewModel permissionListBox,
        ICountryService countryService,
        INotificationService notificationService)
    {
        _employeeService = employeeService;
        PermissionListBox = permissionListBox;
        _roleService = roleService;
        _countryService = countryService;
        _notificationService = notificationService;
    }

    protected override async Task InitializeAsync()
    {
        var permissionListBoxTask = PermissionListBox.InitializeAsync();
        var countriesTask = _countryService.GetSelectListAsync();
        var roleTask = _roleService.GetSelectListAsync();

        await Task.WhenAll(permissionListBoxTask, roleTask, countriesTask);

        var roles = roleTask.Result;
        var countries = countriesTask.Result;

        Roles = new ObservableCollection<SelectListItemResponse<Guid>>(roles.Success ? roles.Value : []);
        Countries = new ObservableCollection<SelectListItemResponse<Guid>>(countries.Success ? countries.Value : []);
    }

    protected override void Clear()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        SelectedRole = null;
        HouseNumber = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        PostalCode = string.Empty;
        City = string.Empty;
        Street = string.Empty;
        Password = string.Empty;
        FlatNumber = string.Empty;
        PermissionListBox.ClearSelectedPermissions();
    }

    public async Task InitializeForEdit(Guid itemId)
    {
        IsEditMode = true;
        EditingItemId = itemId;

        var result = await _employeeService.GetByIdAsync(itemId);
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while loading the employee.");
            return;
        }

        await InitializeAsync();
        EmployeeMappings.MapFromResponse(this, result.Value);
    }

    protected override async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
        {
            _notificationService.ShowError("Password is required for new employees.");
            return;
        }
        var request = EmployeeMappings.MapToAddRequest(this);
        var result = await _employeeService.AddAsync(request);
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while adding the employee.");
            return;
        }

        _notificationService.ShowSuccess("Successfully added employee!");
    }

    protected override async Task UpdateAsync()
    {
        if (EditingItemId is null)
            return;

        var request = EmployeeMappings.MapToUpdateRequest(this);
        var result = await _employeeService.UpdateAsync(EditingItemId.Value, request);
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while updating the employee.");
            return;
        }

        _notificationService.ShowSuccess("Employee updated successfully");
    }

}