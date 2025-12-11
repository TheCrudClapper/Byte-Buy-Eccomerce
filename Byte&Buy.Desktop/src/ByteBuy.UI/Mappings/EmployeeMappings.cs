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
            vm.Street,
            vm.HouseNumber,
            vm.PostalCode,
            vm.City,
            vm.Country,
            vm.FlatNumber,
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
            vm.Street,
            vm.HouseNumber,
            vm.PostalCode,
            vm.City,
            vm.Country,
            vm.PhoneNumber,
            vm.FlatNumber,
            vm.Password,
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
        vm.Street = response.Street;
        vm.HouseNumber = response.HouseNumber;
        vm.FlatNumber = response.FlatNumber;
        vm.PostalCode = response.PostalCode;
        vm.City = response.City;
        vm.Country = response.Country;
        vm.SelectedRole = vm.Roles.FirstOrDefault(r => r.Id == response.RoleId);

        vm.PermissionListBox.SetSelectedPermissions(response.RevokedPermissionIds.ToList(),
            response.GrantedPermissionIds.ToList());
    }
}