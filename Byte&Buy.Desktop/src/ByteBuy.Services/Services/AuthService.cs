using ByteBuy.Services.DTO;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.InfraContracts.Stores;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class AuthService(IAuthHttpClient authHttpClient, ITokenStore tokenStore)
    : IAuthService
{
    public async Task<Result> LoginAsync(LoginRequest request)
    {
        var result = await authHttpClient.LoginAsync(request);

        if (!result.Success)
            return Result.Fail(result.Error!);

        tokenStore.AccessToken = result.Value!.Token;

        return Result.Ok();
    }
}