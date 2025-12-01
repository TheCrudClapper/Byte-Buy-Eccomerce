using ByteBuy.Services.DTO;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IAuthHttpClient
{
    Task<Result<TokenResponse>> LoginAsync(LoginRequest request);
}