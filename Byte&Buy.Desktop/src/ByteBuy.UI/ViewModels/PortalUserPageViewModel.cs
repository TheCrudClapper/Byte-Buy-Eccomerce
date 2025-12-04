using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class PortalUserPageViewModel : ViewModelSingle
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
    [Required]
    [MaxLength(50)]
    private string _label = string.Empty;

    [ObservableProperty][MaxLength(10)] private string? _flatNumber = string.Empty;

    [ObservableProperty][MaxLength(15)] private string? _phoneNumber = string.Empty;

    [ObservableProperty] private string _password = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Choose employee's role")]
    private SelectListItemResponse? _selectedRole;

    [ObservableProperty] private ObservableCollection<SelectListItemResponse> _roles = [];

    [ObservableProperty] private ObservableCollection<SelectListItemResponse> _countries = [];
    [ObservableProperty] private SelectListItemResponse? _selectedCountry;

    public PermissionGrantRevokeViewModel PermissionListBox { get; }
    private readonly IRoleService _roleService;
    private readonly ICountryService _countryService;
    private readonly IPortalUserService _portalUserService;

    #endregion

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
    }

    protected override async Task Save()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;
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
        Roles = new ObservableCollection<SelectListItemResponse>(result.Value!);
    }

    private async Task LoadCountries()
    {
        var result = await _countryService.GetSelectList();
        Countries = new ObservableCollection<SelectListItemResponse>(result.Value!);
    }
}