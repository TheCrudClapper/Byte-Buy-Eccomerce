using ByteBuy.Core.Domain.Shared.Enums;
using ByteBuy.Core.Domain.Users;
using ByteBuy.Core.DTO.Internal.Checkout;
using ByteBuy.Core.DTO.Internal.PortalUser;
using ByteBuy.Core.DTO.Internal.Seller;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.DTO.Public.Shared;
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
        => p => new PortalUserListResponse(
            p.Id,
            p.FirstName,
            p.LastName,
            p.Email!,
            p.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault() ?? "Unknown");

    public static Expression<Func<PortalUser, UserBasicInfoResponse>> UserBasicInfoResponseProjection
        => p => new UserBasicInfoResponse(
            p.FirstName,
            p.LastName,
            p.Email!,
            p.PhoneNumber);

    public static Expression<Func<PortalUser, SellerCheckoutQueryModel>> SellerPortalResponseProjection
        => p => new SellerCheckoutQueryModel(
            p.Id,
            $"{p.FirstName} {p.LastName}",
            p.Email!);

    public static Expression<Func<PortalUser, SellerSnapshotQueryModel>> SellerSnapshotDtoProjection
        => p => new SellerSnapshotQueryModel(
            p.Id,
            SellerType.PrivatePerson,
            $"{p.FirstName} {p.LastName}",
            null,
            p.HomeAddress!);

    public static Expression<Func<PortalUser, PortalUserBuyerQueryModel>> BuyerSnapshotQueryProjection
        => p => new PortalUserBuyerQueryModel(
            p.FirstName,
            p.LastName,
            p.Email!,
            p.PhoneNumber!,
            p.HomeAddress);
}
