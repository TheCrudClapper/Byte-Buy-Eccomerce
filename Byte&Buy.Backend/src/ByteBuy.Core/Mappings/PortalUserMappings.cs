using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.AddressValueObj;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.DTO.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class PortalUserMappings
{
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
            p.HomeAddress == null
            ? null
            : new HomeAddressDto(
                p.HomeAddress.Street,
                p.HomeAddress.HouseNumber,
                p.HomeAddress.PostalCity,
                p.HomeAddress.PostalCode,
                p.HomeAddress.City,
                p.HomeAddress.Country,
                p.HomeAddress.FlatNumber
                ),
            p.UserPermissions.Where(up => up.IsGranted).Select(up => up.PermissionId).ToList(),
            p.UserPermissions.Where(up => !up.IsGranted).Select(up => up.PermissionId).ToList());

    public static Expression<Func<PortalUser, PortalUserListResponse>> PortalUserListResponseProjection
        => p => new PortalUserListResponse(p.Id,
            p.FirstName,
            p.LastName,
            p.Email!,
            p.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault() ?? "Unknown");
}
