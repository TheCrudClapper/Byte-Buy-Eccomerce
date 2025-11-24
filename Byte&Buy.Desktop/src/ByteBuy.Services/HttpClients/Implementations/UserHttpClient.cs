using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations
{
    public class UserHttpClient(HttpClient httpClient) : HttpClientBase(httpClient), IUserHttpClient
    {
        public async Task<Result> ChangePasswordAsync(PasswordChangeRequest request)
            => await PutAsync("users/password", request);
    }
}
