using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.ApplicationUser;

namespace ByteBuy.Core.ServiceContracts;

public interface IApplicationUserService
{
    Task<Result> ChangePasswordAsync(Guid userId, PasswordChangeRequest request);
}
