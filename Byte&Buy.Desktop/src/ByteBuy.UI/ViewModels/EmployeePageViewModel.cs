using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
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

    [ObservableProperty][Required] private string _firstName = string.Empty;

    [ObservableProperty][Required] private string _lastName = string.Empty;

    [ObservableProperty]
    [Required]
    [EmailAddress]
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
    [MaxLength(15)]
    private string? _phoneNumber = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Choose employee's role")]
    private SelectListItemResponse? _selectedRole;

    [ObservableProperty] private ObservableCollection<SelectListItemResponse> _roles = [];

    #endregion

    private readonly IEmployeeService _employeeService;
    private readonly IRoleService _roleService;

    public PermissionGrantRevokeViewModel PermissionListBox { get; }

    public EmployeePageViewModel(
        IRoleService roleService,
        IEmployeeService employeeService,
        PermissionGrantRevokeViewModel permissionListBox,
        AlertViewModel alert) : base(alert)
    {
        _employeeService = employeeService;
        PermissionListBox = permissionListBox;
        _roleService = roleService;
        _ = LoadRoles();
    }

    protected override void Clear()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        SelectedRole = null;
        HouseNumber = string.Empty;
        Email = string.Empty;
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

        var result = await _employeeService.GetById(itemId);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        EmployeeMappings.MapFromResponse(this, value);
    }

    protected override async Task AddItem()
    {
        if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
        {
            Alert.Show(AlertType.Error, "Password is required for new employees.");
            return;
        }
        var request = EmployeeMappings.MapToAddRequest(this);
        var result = await _employeeService.Add(request);
        HandleResult(result, "Successfully added employee!");
    }

    protected override async Task UpdateItem()
    {
        if (EditingItemId is null)
            return;

        var request = EmployeeMappings.MapToUpdateRequest(this);
        var result = await _employeeService.Update(EditingItemId.Value, request);
        HandleResult(result, "Employee updated successfully");
    }

    private async Task LoadRoles()
    {
        var result = await _roleService.GetSelectList();
        if (!result.Success)
            return;

        Roles = new ObservableCollection<SelectListItemResponse>(result.Value ?? []);
    }
}