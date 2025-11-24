using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IUserHttpClient
{
    Task<Result> ChangePasswordAsync(PasswordChangeRequest request);
}
