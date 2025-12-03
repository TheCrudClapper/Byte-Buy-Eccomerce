using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
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

    public static CreatedResponse ToCreatedResponse(this PortalUser user)
        => new CreatedResponse(user.Id, user.DateCreated);

    public static CreatedResponse ToUpdatedResponse(this PortalUser user)
        => new CreatedResponse(user.Id, user.DateEdited ?? DateTime.MinValue);
}
