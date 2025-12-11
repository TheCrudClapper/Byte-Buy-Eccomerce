using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

    public void InitializeForAdd()
    {
        IsEditMode = false;
        EditingItemId = null;
        Clear();
    }

    public async Task InitializeForEdit(Guid itemId)
    {
        IsEditMode = true;
        EditingItemId = itemId;

        var result = await _employeeService.GetById(itemId);
        if (!result.Success)
        {
            Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var item = result.Value;

        FirstName = item.FirstName;
        LastName = item.LastName;
        Email = item.Email;
        PhoneNumber = item.PhoneNumber;
        Street = item.Street;
        HouseNumber = item.HouseNumber;
        FlatNumber = item.FlatNumber;
        PostalCode = item.PostalCode;
        City = item.City;
        Country = item.Country;
        SelectedRole = Roles.FirstOrDefault(r => r.Id == item.RoleId);

        PermissionListBox.SetSelectedPermissions(item.RevokedPermissionIds.ToList(),
            item.GrantedPermissionIds.ToList());
    }

    protected override async Task Save()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        await (IsEditMode switch
        {
            true => UpdateItem(),
            false => AddItem()
        });
    }

    private async Task AddItem()
    {
        if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
        {
            Alert.Show(AlertType.Error, "Password is required for new employees.");
            return;
        }
        var request = new EmployeeAddRequest(
            SelectedRole!.Id,
            FirstName,
            LastName,
            Email,
            Password,
            PhoneNumber,
            Street,
            HouseNumber,
            PostalCode,
            City,
            Country,
            FlatNumber,
            PermissionListBox.ExtractGrantedPermissions(),
            PermissionListBox.ExtractRevokedPermissions()
        );

        var result = await _employeeService.Add(request);
        if (!result.Success)
        {
            Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }
        Alert.ShowSuccessAlert("Employee Added Successfully");
    }

    private async Task UpdateItem()
    {
        if (EditingItemId is null)
            return;

        var request = new EmployeeUpdateRequest(
            SelectedRole!.Id,
            FirstName, LastName,
            Email, Street,
            HouseNumber,
            PostalCode,
            City,
            Country,
            PhoneNumber,
            FlatNumber,
            Password,
            PermissionListBox.ExtractGrantedPermissions(),
            PermissionListBox.ExtractRevokedPermissions());

        var result = await _employeeService.Update(EditingItemId.Value, request);
        if (!result.Success)
        {
            Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }
        Alert.Show(AlertType.Success, "Employee updated successfully");
    }

    private async Task LoadRoles()
    {
        var result = await _roleService.GetSelectList();
        if (!result.Success)
            return;

        Roles = new ObservableCollection<SelectListItemResponse>(result.Value ?? []);
    }
}