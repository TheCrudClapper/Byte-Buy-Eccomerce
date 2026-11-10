using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Public;

public interface IAuthHttpClient
{
    Task<Result<LoginResponse>> LoginAsync(LoginRequest request);
}