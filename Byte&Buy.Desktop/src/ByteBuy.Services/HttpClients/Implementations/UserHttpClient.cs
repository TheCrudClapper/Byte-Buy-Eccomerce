using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBuy.Services.HttpClients.Implementations
{
    public class UserHttpClient : HttpClientBase, IUserHttpClient
    {
        public UserHttpClient(HttpClient httpClient) : base(httpClient){}

        public async Task<Result> ChangePassword(PasswordChangeRequest request)
            => await PutAsync("users/password", request);
    }
}
