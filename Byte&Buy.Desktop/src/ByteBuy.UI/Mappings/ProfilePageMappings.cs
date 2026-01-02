using ByteBuy.Services.DTO.Employee;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Mappings;

public static class ProfilePageMappings
{
    public static void MapFromResponse(this ProfilePageViewModel vm, EmployeeProfileResponse response)
    {
        
        vm.FirstName = response.FirstName;
        vm.LastName = response.LastName;
        vm.RoleName = response.RoleName;
        vm.PostalCity = response.HomeAddress.PostalCity;
        vm.City = response.HomeAddress.City;
        vm.HouseNumber = response.HomeAddress.HouseNumber;
        vm.PostalCode = response.HomeAddress.PostalCode;
        vm.FlatNumber = response.HomeAddress.FlatNumber;
        vm.Country = response.HomeAddress.Country;
        vm.Email = response.Email;
        vm.Street = response.HomeAddress.Street;
        vm.PhoneNumber = response.PhoneNumber;
    }
}
