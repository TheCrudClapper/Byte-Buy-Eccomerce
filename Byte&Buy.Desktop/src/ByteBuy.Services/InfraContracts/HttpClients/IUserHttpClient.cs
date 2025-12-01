using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IUserHttpClient
{
    Task<Result> ChangePasswordAsync(PasswordChangeRequest request);
}
