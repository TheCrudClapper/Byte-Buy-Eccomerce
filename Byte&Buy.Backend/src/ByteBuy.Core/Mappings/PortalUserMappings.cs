using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.PortalUser;

namespace ByteBuy.Core.Mappings;

public static class PortalUserMappings
{
    public static PortalUserListResponse ToPortalUserListResponse(this PortalUser user)
    {
        return new PortalUserListResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email!,
            user.UserRoles?.FirstOrDefault()?.Role.Name ?? ""
            );
    }

    //Add nullable for addres bc user don't have to add address while registering
    public static PortalUserResponse ToPortalUserResponse(this PortalUser user)
    {
        var defaultAddress = user.Addresses?
         .FirstOrDefault(a => a.IsDefault)
         ?.ToAddressResponse() ?? null;

        var roles = user.UserRoles;
        var roleId = roles?.FirstOrDefault()?.Role?.Id ?? Guid.Empty;

        var permissions = user.UserPermissions ?? [];
        var grantedPermissions = permissions
            .Where(p => p.IsGranted)
            .Select(p => p.PermissionId)
            .ToList();

        var deniedPermissions = permissions
            .Where(p => !p.IsGranted)
            .Select(p => p.PermissionId)
            .ToList();

        return new PortalUserResponse(
            user.Id,
            roleId,
            user.FirstName,
            user.LastName,
            user.Email!,
            user.PhoneNumber,
            defaultAddress,
            grantedPermissions,
            deniedPermissions
        );
    }
    public static CreatedResponse ToCreatedResponse(this PortalUser user)
        => new CreatedResponse(user.Id, user.DateCreated);

    public static CreatedResponse ToUpdatedResponse(this PortalUser user)
        => new CreatedResponse(user.Id, user.DateEdited ?? DateTime.MinValue);
}
