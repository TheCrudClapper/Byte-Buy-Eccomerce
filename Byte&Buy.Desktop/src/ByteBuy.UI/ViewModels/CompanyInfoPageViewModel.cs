using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public sealed partial class CompanyInfoPageViewModel : PageViewModel
{
    #region MVVM Fields

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _companyName = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(20)]
    private string _tin = string.Empty;

    [ObservableProperty]
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    private string _email = string.Empty;

    [ObservableProperty][Required][MaxLength(30)] private string _slogan = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _street = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(20)]
    private string _houseNumber = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _postalCode = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _city = string.Empty;

    [ObservableProperty]
    [Required]
    [MaxLength(50)]
    private string _country = string.Empty;

    [ObservableProperty][MaxLength(10)] private string? _flatNumber = string.Empty;

    [ObservableProperty][Required][MaxLength(16)] private string _phoneNumber = string.Empty;

    #endregion

    private readonly ICompanyInfoService _companyInfoService;

    public CompanyInfoPageViewModel(AlertViewModel alert,
        ICompanyInfoService companyInfoService) : base(alert)
    {
        _companyInfoService = companyInfoService;
        _ = LoadData();
    }

    [RelayCommand]
    private void Clear()
    {
        CompanyName = string.Empty;
        Slogan = string.Empty;
        Tin = string.Empty;
        HouseNumber = string.Empty;
        Email = string.Empty;
        PostalCode = string.Empty;
        City = string.Empty;
        Street = string.Empty;
        FlatNumber = string.Empty;
        Country = string.Empty;
        PhoneNumber = string.Empty;
    }

    public async Task LoadData()
    {
        var response = await _companyInfoService.GetCompanyInfo();
        var (ok, value) = HandleResult(response);
        if (!ok || value is null)
            return;

        CompanyInfoMappings.MapFromResponse(this, value);
    }

    [RelayCommand]
    private async Task Save()
    {
        ValidateAllProperties();

        if (HasErrors)
            return;

        var request = CompanyInfoMappings.MapToUpdateRequest(this);
        var result = await _companyInfoService.Update(request);
        HandleResult(result, "Successfully updated company details !");
    }
}