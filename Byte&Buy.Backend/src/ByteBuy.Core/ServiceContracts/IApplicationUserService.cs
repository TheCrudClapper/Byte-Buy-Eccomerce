using ByteBuy.Core.DTO.ApplicationUser;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IApplicationUserService
{
    Task<Result> ChangePassword(Guid userId, PasswordChangeRequest request);
    Task<Result<UserBasicInfoResponse>> GetBasicUserInfoAsync(Guid userId, CancellationToken ct = default);
}
