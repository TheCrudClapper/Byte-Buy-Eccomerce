using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.DTO.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class PortalUserMappings
{

    //DEPRECATED 

    //public static PortalUserListResponse ToPortalUserListResponse(this PortalUser user)
    //{
    //    return new PortalUserListResponse(
    //        user.Id,
    //        user.FirstName,
    //        user.LastName,
    //        user.Email!,
    //        user.UserRoles?.FirstOrDefault()?.Role.Name ?? ""
    //        );
    //}

    //AddAsync nullable for addres bc user don't have to add address while registering
    //public static PortalUserResponse ToPortalUserResponse(this PortalUser user)
    //{
    //    var defaultAddress = user.Addresses?
    //     .FirstOrDefault(a => a.IsDefault)
    //     ?.ToAddressResponse() ?? null;

    //    var roles = user.UserRoles;
    //    var roleId = roles?.FirstOrDefault()?.Role?.Id ?? Guid.Empty;

    //    var permissions = user.UserPermissions ?? [];
    //    var grantedPermissions = permissions
    //        .Where(p => p.IsGranted)
    //        .Select(p => p.PermissionId)
    //        .ToList();

    //    var deniedPermissions = permissions
    //        .Where(p => !p.IsGranted)
    //        .Select(p => p.PermissionId)
    //        .ToList();

    //    return new PortalUserResponse(
    //        user.Id,
    //        roleId,
    //        user.FirstName,
    //        user.LastName,
    //        user.Email!,
    //        user.PhoneNumber,
    //        defaultAddress,
    //        grantedPermissions,
    //        deniedPermissions
    //    );
    //}

    public static CreatedResponse ToCreatedResponse(this PortalUser user)
        => new CreatedResponse(user.Id, user.DateCreated);

    public static UpdatedResponse ToUpdatedResponse(this PortalUser user)
        => new UpdatedResponse(user.Id, user.DateEdited ?? DateTime.MinValue);


    //LINQ to Enitites
    public static Expression<Func<PortalUser, PortalUserResponse>> PortalUserResponseProjection
        => p => new PortalUserResponse(p.Id,
            p.UserRoles.Select(ur => ur.RoleId).FirstOrDefault(),
            p.FirstName,
            p.LastName,
            p.Email!,
            p.PhoneNumber,
            p.Addresses
                .Where(a => a.IsDefault)
                .Select(a => new AddressResponse(
                    a.Id,
                    a.CountryId,
                    a.Label,
                    a.Street,
                    a.HouseNumber,
                    a.PostalCity,
                    a.PostalCode,
                    a.City,
                    a.FlatNumber,
                    a.IsDefault
                    )).FirstOrDefault(),
            p.UserPermissions.Where(up => up.IsGranted).Select(up => up.PermissionId).ToList(),
            p.UserPermissions.Where(up => !up.IsGranted).Select(up => up.PermissionId).ToList());

    public static Expression<Func<PortalUser, PortalUserListResponse>> PortalUserListResponseProjection
        => p => new PortalUserListResponse(p.Id,
            p.FirstName,
            p.LastName,
            p.Email!,
            p.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault() ?? "Unknown");
}
