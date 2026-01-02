using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.UI.ModelsUI.Employee;
using ByteBuy.UI.ViewModels;
using System.Linq;

namespace ByteBuy.UI.Mappings;

public static class EmployeeMappings
{
    public static EmployeeListItem ToListItem(this EmployeeListResponse employee, int index)
    {
        return new EmployeeListItem()
        {
            Id = employee.Id,
            RowNumber = index + 1,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Role = employee.Role,
            Email = employee.Email,
        };
    }

    public static EmployeeAddRequest MapToAddRequest(this EmployeePageViewModel vm)
    {
        return new EmployeeAddRequest(
            vm.SelectedRole!.Id,
            vm.FirstName,
            vm.LastName,
            vm.Email,
            vm.Password,
            vm.PhoneNumber,
            MapAddress(vm),
            vm.PermissionListBox.ExtractGrantedPermissions(),
            vm.PermissionListBox.ExtractRevokedPermissions()
            );
    }


    public static EmployeeUpdateRequest MapToUpdateRequest(this EmployeePageViewModel vm)
    {
        return new EmployeeUpdateRequest(
            vm.SelectedRole!.Id,
            vm.FirstName,
            vm.LastName,
            vm.Email,
            MapAddress(vm),
            vm.Password,
            vm.PhoneNumber,
            vm.PermissionListBox.ExtractGrantedPermissions(),
            vm.PermissionListBox.ExtractRevokedPermissions()
            );
    }

    public static void MapFromResponse(this EmployeePageViewModel vm, EmployeeResponse response)
    {
        vm.FirstName = response.FirstName;
        vm.LastName = response.LastName;
        vm.Email = response.Email;
        vm.PhoneNumber = response.PhoneNumber;
        vm.Street = response.HomeAddress.Street;
        vm.HouseNumber = response.HomeAddress.HouseNumber;
        vm.FlatNumber = response.HomeAddress.FlatNumber;
        vm.PostalCity = response.HomeAddress.PostalCity;
        vm.PostalCode = response.HomeAddress.PostalCode;
        vm.City = response.HomeAddress.City;
        vm.SelectedCountry = vm.Countries.FirstOrDefault(c => c.Title == response.HomeAddress.Country);
        vm.SelectedRole = vm.Roles.FirstOrDefault(r => r.Id == response.RoleId);
        vm.PermissionListBox.SetSelectedPermissions(response.RevokedPermissionIds.ToList(),
            response.GrantedPermissionIds.ToList());
    }

    public static HomeAddressDto MapAddress(this EmployeePageViewModel vm)
    {
        return new HomeAddressDto
        {
            FlatNumber = vm.FlatNumber,
            Country = vm.SelectedCountry?.Title ?? "Unknown",
            HouseNumber = vm.HouseNumber,
            City = vm.City,
            PostalCity = vm.PostalCity,
            Street = vm.Street,
            PostalCode = vm.PostalCode
        };
    }
}