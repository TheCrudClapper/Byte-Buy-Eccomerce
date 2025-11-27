using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Permission;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.Services.Services;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeePageViewModel : PageViewModel
{
    #region Fields

    [ObservableProperty] [Required] private string _firstName = string.Empty;

    [ObservableProperty] [Required] private string _lastName = string.Empty;

    [ObservableProperty] [Required] [EmailAddress]
    private string _email = string.Empty;

    [ObservableProperty] [Required] [MaxLength(50)]
    private string _street = string.Empty;

    [ObservableProperty] [Required] [MaxLength(10)]
    private string _houseNumber = string.Empty;

    [ObservableProperty] [Required] [MaxLength(20)]
    private string _postalCode = string.Empty;

    [ObservableProperty] [Required] [MaxLength(50)]
    private string _city = string.Empty;

    [ObservableProperty] [Required] [MaxLength(50)]
    private string _country = string.Empty;

    [ObservableProperty] [MaxLength(10)] private string? _flatNumber = string.Empty;

    [ObservableProperty] [MaxLength(15)] private string? _phoneNumber = string.Empty;

    [ObservableProperty] [Required] [MinLength(8)]
    private string _password = string.Empty;

    [ObservableProperty] [Required(ErrorMessage = "Choose employee's role")]
    private SelectListItemResponse? _selectedRole;

    #endregion

    private bool _isEditMode = false;
    public Guid? _editingItemId = Guid.Empty;
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly EmployeeService _employeeService;
    private readonly RoleService _roleService;
    private readonly IPermissionService _permissionService;
    private PageFactory _pageFactory;

    [ObservableProperty] private ObservableCollection<PermissionItem> _permissions = [];

    [ObservableProperty] private ObservableCollection<SelectListItemResponse> _roles = [];

    public EmployeePageViewModel(
        RoleService roleService,
        MainWindowViewModel main,
        EmployeeService employeeService,
        IPermissionService permissionService,
        PageFactory pageFactory,
        AlertViewModel alert) : base(alert)
    {
        _employeeService = employeeService;
        _permissionService = permissionService;
        _roleService = roleService;
        _pageFactory = pageFactory;
        _mainWindowViewModel = main;
        PageName = ApplicationPageNames.Employee;
        _ = LoadPermissions();
        _ = LoadRoles();
    }

    [RelayCommand]
    private void Clear()
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
    }

    public void InitializeForAdd()
    {
        _isEditMode = false;
        _editingItemId = null;

        Clear();
    }

    public async Task InitializeForEdit(Guid id)
    {
        _isEditMode = true;
        _editingItemId = id;

        var result = await _employeeService.GetById(id);
        if (!result.Success)
        {
            await Alert.Show(AlertType.Error, result.Error!.Description);
        }

        var item = result.Value!;

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
    }

    [RelayCommand]
    private async Task Save()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;
        if (_isEditMode)
        {
            await UpdateItem();
        }
        else
        {
            await AddItem();
        }
    }

    private async Task AddItem()
    {
        var request = new EmployeeAddRequest(SelectedRole!.Id, FirstName, LastName, Email, Password, PhoneNumber,
            Street,
            HouseNumber, PostalCode, City, Country, FlatNumber);

        var result = await _employeeService.Add(request);
        if (!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);
        else
            await Alert.Show(AlertType.Success, "Employee Added Successfully");
    }

    private async Task UpdateItem()
    {
        if(_editingItemId is null)
            return;

        var request = new EmployeeUpdateRequest(
            SelectedRole!.Id,
            FirstName, LastName,
            Email, Street,
            HouseNumber,
            PostalCode,
            City,
            Country,
            PhoneNumber!,
            FlatNumber
            );

        var result = await _employeeService.Update(_editingItemId.Value,  request);
        if (!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);
        
        await Alert.Show(AlertType.Success, "Employee updated successfully");
        
    }

    private async Task LoadPermissions()
    {
        var result = await _permissionService.GetSelectList();
        var permissionItems = result.Value!
            .Select(p => new PermissionItem(p.Id, p.Title, false))
            .ToList();
        Permissions = new ObservableCollection<PermissionItem>(permissionItems);
    }

    private async Task LoadRoles()
    {
        var result = await _roleService.GetSelectList();
        Roles = new ObservableCollection<SelectListItemResponse>(result.Value!);
    }
}