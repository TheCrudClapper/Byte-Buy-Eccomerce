using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Mappings;

public static class CompanyInfoMappings
{
    public static void MapFromResponse(this CompanyInfoPageViewModel vm, CompanyInfoResponse response)
    {
        vm.CompanyName = response.CompanyName;
        vm.Slogan = response.Slogan;
        vm.Tin = response.TIN;
        vm.HouseNumber = response.Address.HouseNumber;
        vm.Email = response.Email;
        vm.PostalCode = response.Address.PostalCode;
        vm.City = response.Address.City;
        vm.Street = response.Address.Street;
        vm.FlatNumber = response.Address.FlatNumber;
        vm.Country = response.Address.Country;
        vm.PhoneNumber = response.PhoneNumber;
    }

    public static CompanyInfoUpdateRequest MapToUpdateRequest(this CompanyInfoPageViewModel vm)
    {
        return new CompanyInfoUpdateRequest
        {
            Email = vm.Email,
            CompanyName = vm.CompanyName,
            PhoneNumber = vm.PhoneNumber,
            TIN = vm.Tin,
            Slogan = vm.Slogan,
            Address = new AddressDto()
            {
                City = vm.City,
                PostalCode = vm.PostalCode,
                Country = vm.Country,
                FlatNumber = vm.FlatNumber,
                Street = vm.Street,
                HouseNumber = vm.HouseNumber
            }
        };
    }
}
