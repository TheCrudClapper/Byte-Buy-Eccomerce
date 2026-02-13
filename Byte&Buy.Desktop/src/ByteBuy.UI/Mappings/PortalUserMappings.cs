using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.UI.ModelsUI.PortalUser;
using ByteBuy.UI.ViewModels;
using System;
using System.Linq;

namespace ByteBuy.UI.Mappings;

public static class PortalUserMappings
{
    /// <summary>
    /// Maps dto to ui model used in list
    /// </summary>
    /// <param name="user"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static PortalUserListItem ToListItem(this PortalUserListResponse user, int index)
    {
        return new PortalUserListItem
        {
            RowNumber = index,
            Role = user.Role,
            Email = user.Email,
            LastName = user.LastName,
            Id = user.Id,
            FirstName = user.FirstName
        };
    }

    /// <summary>
    /// Maps viewmodel fields to add request
    /// </summary>
    /// <param name="vm">View model to copy properties value from</param>
    /// <returns>Ready to use DTO</returns>
    public static PortalUserAddRequest MapToAddRequest(PortalUserPageViewModel vm)
    {
        var address = MapToHomeAddress(vm);
        var request = new PortalUserAddRequest
            (
                vm.SelectedRole!.Id,
                vm.FirstName,
                vm.LastName,
                vm.Email,
                vm.Password,
                vm.PhoneNumber,
                address,
                vm.PermissionListBox.ExtractGrantedPermissions(),
                vm.PermissionListBox.ExtractRevokedPermissions()
            );

        return request;
    }

    /// <summary>
    /// Maps viewmodel fields to update request
    /// </summary>
    /// <param name="vm">View model to copy properties value from</param>
    /// <returns>Ready to use DTO</returns>
    public static PortalUserUpdateRequest MapToUpdateRequest(PortalUserPageViewModel vm)
    {
        var request = new PortalUserUpdateRequest(
                vm.SelectedRole!.Id,
                vm.FirstName,
                vm.LastName,
                vm.Email,
                vm.Password,
                vm.PhoneNumber,
                vm.PermissionListBox.ExtractGrantedPermissions(),
                vm.PermissionListBox.ExtractRevokedPermissions()
            );

        return request;
    }

    public static HomeAddressDto MapToHomeAddress(this PortalUserPageViewModel vm)
    {
        return new HomeAddressDto
        {
            City = vm.City,
            Country = vm.SelectedCountry?.Title ?? "Unknown",
            FlatNumber = vm.FlatNumber,
            Street = vm.Street,
            HouseNumber = vm.HouseNumber,
            PostalCode = vm.PostalCode,
            PostalCity = vm.PostalCity
        };
    }

    /// <summary>
    /// Maps returned api response to view model properties
    /// </summary>
    /// <param name="vm">View model to copy to</param>
    /// <param name="response">Response to copy properties from</param>
    public static void MapFromResponse(PortalUserPageViewModel vm, PortalUserResponse response)
    {
        vm.FirstName = response.FirstName;
        vm.LastName = response.LastName;
        vm.SelectedCountry = vm.Countries
            .FirstOrDefault(c => c.Title == response.HomeAddress?.Country);
        vm.SelectedRole = vm.Roles
            .FirstOrDefault(r => r.Id == response.RoleId);
        vm.Email = response.Email;
        vm.PhoneNumber = response.PhoneNumber;
        vm.Street = response.HomeAddress?.Street ?? string.Empty;
        vm.HouseNumber = response.HomeAddress?.HouseNumber ?? string.Empty;
        vm.FlatNumber = response.HomeAddress?.FlatNumber;
        vm.City = response.HomeAddress?.City ?? string.Empty;
        vm.PostalCode = response.HomeAddress?.PostalCode ?? string.Empty;
        vm.PostalCity = response.HomeAddress?.PostalCity ?? string.Empty;
        vm.PermissionListBox.SetSelectedPermissions(response.RevokedPermissionIds.ToList(),
            response.GrantedPermissionIds.ToList());
    }
}