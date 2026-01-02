using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.DataAdnotations;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class PortalUserPageViewModel : ViewModelSingle
{
    #region MVVM Fields

    [ObservableProperty]
    [Required]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [Required] private string _lastName = string.Empty;

    [ObservableProperty]
    [Required]
    [EmailAddress]
    private string _email = string.Empty;

    [ObservableProperty]
    [RequiredIf(nameof(IsAddressIncluded))]
    [MaxLength(50)]
    private string _street = string.Empty;

    [ObservableProperty]
    [RequiredIf(nameof(IsAddressIncluded))]
    [MaxLength(10)]
    private string _houseNumber = string.Empty;

    [ObservableProperty]
    [RequiredIf(nameof(IsAddressIncluded))]
    [MaxLength(20)]
    private string _postalCode = string.Empty;

    [ObservableProperty]
    [RequiredIf(nameof(IsAddressIncluded))]
    [MaxLength(50)]
    private string _postalCity = string.Empty;

    [ObservableProperty]
    [RequiredIf(nameof(IsAddressIncluded))]
    [MaxLength(50)]
    private string _city = string.Empty;

    [ObservableProperty]
    private string? _flatNumber = string.Empty;

    [ObservableProperty]
    [MaxLength(15)]
    private string? _phoneNumber;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<Guid>> _roles = [];

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<Guid>> _countries = [];

    [ObservableProperty]
    [RequiredIf(nameof(IsAddressIncluded))]
    private SelectListItemResponse<Guid>? _selectedCountry;

    [ObservableProperty]
    [Required(ErrorMessage = "Choose user's role")]
    private SelectListItemResponse<Guid>? _selectedRole;

    [ObservableProperty]
    private bool _isAddressIncluded = true;

    public PermissionGrantRevokeViewModel PermissionListBox { get; }
    #endregion

    private readonly IRoleService _roleService;
    private readonly ICountryService _countryService;
    private readonly IPortalUserService _portalUserService;
    private readonly IHomeAddressService _addressService;
    public PortalUserPageViewModel(AlertViewModel alert,
        PermissionGrantRevokeViewModel listBox,
        IRoleService roleService,
        IPortalUserService userService,
        IHomeAddressService addressService,
        ICountryService countryService) : base(alert)
    {
        PermissionListBox = listBox;
        _roleService = roleService;
        _addressService = addressService;
        _countryService = countryService;
        _portalUserService = userService;
    }

    protected override async Task UpdateItem()
    {
        if (EditingItemId is null)
            return;

        var request = PortalUserMappings.MapToUpdateRequest(this);
        var result = await _portalUserService.Update(EditingItemId.Value, request);
        HandleResult(result, "Successfully updated user!");
    }

    protected override async Task InitializeAsync()
    {
        var listBoxTask = PermissionListBox.InitializeAsync();
        var countriesTask = _countryService.GetSelectList();
        var roleTask = _roleService.GetSelectList();

        await Task.WhenAll(listBoxTask, countriesTask, roleTask);

        var countries = await countriesTask;
        var roles = await roleTask;
        await listBoxTask;

        Countries = new ObservableCollection<SelectListItemResponse<Guid>>(countries.Value ?? []);

        Roles = new ObservableCollection<SelectListItemResponse<Guid>>(roles.Value ?? []);
    }

    protected override async Task AddItem()
    {
        if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
        {
            Alert.ShowErrorAlert("For new user password is required !");
            return;
        }

        var request = PortalUserMappings.MapToAddRequest(this);
        var result = await _portalUserService.Add(request);
        HandleResult(result, "Successfullt added new user!");
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
        SelectedCountry = null;
        SelectedRole = null;
        PermissionListBox.ClearSelectedPermissions();
    }

    [RelayCommand]
    private async Task SaveAddress()
    {
        ValidateAllProperties();
        var address = PortalUserMappings.MapToHomeAddress(this);
        var result = await _addressService.SetHomeAddress(EditingItemId.GetValueOrDefault(), address);
        HandleResult(result, "Successfully assigned address for user");
    }

    public async Task InitializeForEdit(Guid itemId)
    {
        EditingItemId = itemId;
        IsEditMode = true;
        await InitializeAsync();

        var result = await _portalUserService.GetById(itemId);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        IsAddressIncluded = value.HomeAddress is not null;
        PortalUserMappings.MapFromResponse(this, value);
    }

    partial void OnIsAddressIncludedChanged(bool value)
    {
        if (!value)
        {
            ClearErrors(nameof(Street));
            ClearErrors(nameof(HouseNumber));
            ClearErrors(nameof(PostalCode));
            ClearErrors(nameof(City));
            ClearErrors(nameof(PostalCity));
            ClearErrors(nameof(SelectedCountry));
        }
        else
        {
            ValidateProperty(Street, nameof(Street));
            ValidateProperty(HouseNumber, nameof(HouseNumber));
            ValidateProperty(PostalCode, nameof(PostalCode));
            ValidateProperty(City, nameof(City));
            ValidateProperty(PostalCity, nameof(PostalCity));
            ValidateProperty(SelectedCountry, nameof(SelectedCountry));
        }
    }
}