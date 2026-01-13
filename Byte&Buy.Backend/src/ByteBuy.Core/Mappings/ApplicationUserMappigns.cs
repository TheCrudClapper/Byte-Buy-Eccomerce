using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.ApplicationUser;

namespace ByteBuy.Core.Mappings;

public static class ApplicationUserMappigns
{
    public static UserBasicInfoResponse ToUserBasicInfoResponse(this ApplicationUser user)
    {
        return new UserBasicInfoResponse(
            user.FirstName,
            user.LastName,
            user.Email!,
            user.PhoneNumber
            );
    }
}
