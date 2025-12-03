using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.UI.ModelsUI.PortalUser;

namespace ByteBuy.UI.Mappings;

public static class PortalUserMappings
{
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
}