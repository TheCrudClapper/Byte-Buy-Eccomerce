using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IEmployeeHttpClient
{
    Task<Result<EmployeeResponse>> GetSelfAsync();
}