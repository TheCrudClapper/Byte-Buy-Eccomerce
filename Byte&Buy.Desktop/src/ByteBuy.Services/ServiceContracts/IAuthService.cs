using ByteBuy.Services.DTO;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IAuthService
{
    Task<Result> Login(LoginRequest request);
}