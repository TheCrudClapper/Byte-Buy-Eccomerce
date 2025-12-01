using ByteBuy.Services.DTO;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.InfraContracts.Stores;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class AuthService
    (IAuthHttpClient authHttpClient, ITokenStore tokenStore) : IAuthService
{
    public async Task<Result> Login(LoginRequest request)
    {
        var result = await authHttpClient.LoginAsync(request);

        if (!result.Success)
            return Result.Fail(result.Error!);

        tokenStore.AccessToken = result.Value!.Token;

        return Result.Ok();
    }
}