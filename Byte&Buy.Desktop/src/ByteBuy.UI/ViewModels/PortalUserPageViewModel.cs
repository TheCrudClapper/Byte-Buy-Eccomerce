using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.DataAdnotations;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
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
    [RequiredIf(nameof(IsAddressIncluded))]
    private Guid? _addressEditId = Guid.Empty;

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
    [RequiredIf(nameof(IsAddressIncluded))]
    [MaxLength(50)]
    private string _label = string.Empty;

    [ObservableProperty]
    private string? _flatNumber = string.Empty;

    [ObservableProperty]
    [MaxLength(15)]
    private string? _phoneNumber;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse> _roles = [];

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse> _countries = [];

    [ObservableProperty]
    [RequiredIf(nameof(IsAddressIncluded))]
    private SelectListItemResponse? _selectedCountry;

    [ObservableProperty]
    [Required(ErrorMessage = "Choose user's role")]
    private SelectListItemResponse? _selectedRole;

    [ObservableProperty]
    private bool _isAddressIncluded = true;

    public PermissionGrantRevokeViewModel PermissionListBox { get; }
    #endregion

    private readonly IRoleService _roleService;
    private readonly ICountryService _countryService;
    private readonly IPortalUserService _portalUserService;
    public PortalUserPageViewModel(AlertViewModel alert,
        PermissionGrantRevokeViewModel listBox,
        IRoleService roleService,
        IPortalUserService userService,
        ICountryService countryService) : base(alert)
    {
        PermissionListBox = listBox;
        _roleService = roleService;
        _countryService = countryService;
        _portalUserService = userService;
        _ = LoadCountries();
        _ = LoadRoles();
    }

    protected override async Task UpdateItem()
    {
        if (EditingItemId is null)
            return;

        var request = PortalUserMappings.MapToUpdateRequest(this);
        var result = await _portalUserService.Update(EditingItemId.Value, request);
        HandleResult(result, "Successfully updated user!");
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

    private async Task LoadRoles()
    {
        var result = await _roleService.GetSelectList();
        if (!result.Success)
            return;

        Roles = new ObservableCollection<SelectListItemResponse>(result.Value ?? []);
    }

    private async Task LoadCountries()
    {
        var result = await _countryService.GetSelectList();
        if (!result.Success)
            return;

        Countries = new ObservableCollection<SelectListItemResponse>(result.Value ?? []);
    }

    public void InitializeForAdd()
    {
        EditingItemId = Guid.Empty;
        IsEditMode = false;
        Clear();
    }

    public async Task InitializeForEdit(Guid itemId)
    {
        EditingItemId = itemId;
        IsEditMode = true;

        var result = await _portalUserService.GetById(itemId);
        var (ok,value) = HandleResult(result);
        if (!ok || value is null)
            return;

        IsAddressIncluded = value.Address is null ? false : true;
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
            ClearErrors(nameof(Label));
        }
        else
        {
            ValidateProperty(Street, nameof(Street));
            ValidateProperty(HouseNumber, nameof(HouseNumber));
            ValidateProperty(PostalCode, nameof(PostalCode));
            ValidateProperty(City, nameof(City));
            ValidateProperty(PostalCity, nameof(PostalCity));
            ValidateProperty(SelectedCountry, nameof(SelectedCountry));
            ValidateProperty(Label, nameof(Label));
        }
    }
}