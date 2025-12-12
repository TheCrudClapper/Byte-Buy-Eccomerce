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
        vm.City = response.City;
        vm.HouseNumber = response.HouseNumber;
        vm.PostalCode = response.PostalCode;
        vm.FlatNumber = response.FlatNumber;
        vm.Country = response.Country;
        vm.Email = response.Email;
        vm.Street = response.Street;
        vm.PhoneNumber = response.PhoneNumber;
    }
}
