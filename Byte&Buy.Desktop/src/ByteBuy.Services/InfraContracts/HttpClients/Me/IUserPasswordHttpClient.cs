using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Me;

public interface IUserPasswordHttpClient
{
    Task<Result> PutPasswordAsync(PasswordChangeRequest request);
}
