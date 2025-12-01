using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public sealed partial class CompanyInfoPageViewModel : ViewModelSingle
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
        PageName = ApplicationPageNames.CompanyInfo;
        _companyInfoService = companyInfoService;
        _ = LoadData();
    }

    protected override void Clear()
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

    private async Task LoadData()
    {
        var response = await _companyInfoService.GetCompanyInfo();
        if (!response.Success)
            await Alert.Show(AlertType.Error, response.Error!.Description);

        if (response.Value is null)
            return;

        var companyInfo = response.Value;
        CompanyName = companyInfo.CompanyName;
        Slogan = companyInfo.Slogan;
        Tin = companyInfo.TIN;
        HouseNumber = companyInfo.Address.HouseNumber;
        Email = companyInfo.Email;
        PostalCode = companyInfo.Address.PostalCode;
        City = companyInfo.Address.City;
        Street = companyInfo.Address.Street;
        FlatNumber = companyInfo.Address.FlatNumber;
        Country = companyInfo.Address.Country;
        PhoneNumber = companyInfo.PhoneNumber;
    }

    protected override async Task Save()
    {
        ValidateAllProperties();

        if (HasErrors)
            return;

        var request = new CompanyInfoUpdateRequest
        {
            Email = Email,
            CompanyName = CompanyName,
            PhoneNumber = PhoneNumber,
            TIN = Tin,
            Slogan = Slogan,
            Address = new AddressDto()
            {
                City = City,
                PostalCode = PostalCode,
                Country = Country,
                FlatNumber = FlatNumber,
                Street = Street,
                HouseNumber = HouseNumber
            }
        };

        var response = await _companyInfoService.UpdateCompanyInfo(request);
        if (!response.Success)
            await Alert.Show(AlertType.Error, response.Error!.Description);

        await Alert.Show(AlertType.Success, "Updated Details Successfully");
    }
}