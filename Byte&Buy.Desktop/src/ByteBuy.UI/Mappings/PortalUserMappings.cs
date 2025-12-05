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
            RowNumber = index + 1,
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
        var address = new AddressAddRequest(
                vm.SelectedCountry!.Id,
                vm.Label,
                vm.Street,
                vm.HouseNumber,
                vm.PostalCode,
                vm.PostalCity,
                vm.City,
                vm.FlatNumber
                );

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
        AddressUpdateRequest? address;
        if (vm.IsAddressIncluded)
        {
            address = new AddressUpdateRequest(
              vm.AddressEditId!.Value,
              vm.SelectedCountry!.Id,
              vm.Label,
              vm.Street,
              vm.HouseNumber,
              vm.PostalCode,
              vm.PostalCity,
              vm.City,
              vm.FlatNumber
          );
        }
        else
            address = null;
        

        var request = new PortalUserUpdateRequest(
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
    /// Maps returned api response to view model properties
    /// </summary>
    /// <param name="vm">View model to copy to</param>
    /// <param name="response">Response to copy properties from</param>
    public static void MapFromResponse(PortalUserPageViewModel vm, PortalUserResponse response)
    {
        vm.FirstName = response.FirstName;
        vm.LastName = response.LastName;
        vm.SelectedCountry = vm.Countries
            .Where(c => c.Id == response.Address?.CountryId)
            .FirstOrDefault();
        vm.SelectedRole = vm.Roles
            .Where(r => r.Id == response.RoleId)
            .FirstOrDefault();
        vm.Email = response.Email;
        vm.PhoneNumber = response.PhoneNumber;
        vm.Street = response.Address?.Street ?? string.Empty;
        vm.HouseNumber = response.Address?.HouseNumber ?? string.Empty;
        vm.FlatNumber = response.Address?.FlatNumber;
        vm.AddressEditId = response.Address?.Id ?? Guid.Empty;
        vm.City = response.Address?.City ?? string.Empty;
        vm.PostalCode = response.Address?.PostalCode ?? string.Empty;
        vm.PostalCity = response.Address?.PostalCity ?? string.Empty;
        vm.Label = response.Address?.Label ?? string.Empty;
        vm.PermissionListBox.SetSelectedPermissions(response.RevokedPermissionIds.ToList(),
            response.GrantedPermissionIds.ToList());
    }
}