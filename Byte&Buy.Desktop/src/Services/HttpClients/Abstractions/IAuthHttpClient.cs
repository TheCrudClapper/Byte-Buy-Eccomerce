using ByteBuy.Services.DTO;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IAuthHttpClient
{
    Task<Result<TokenResponse>> LoginAsync(LoginRequest request);
}