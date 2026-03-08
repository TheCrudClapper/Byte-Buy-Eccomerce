using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IApplicationUserService
{
    Task<Result> ChangePasswordAsync(Guid userId, PasswordChangeRequest request);
}
