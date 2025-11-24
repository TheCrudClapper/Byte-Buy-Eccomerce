using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class EmployeeService(IEmployeeHttpClient employeeHttpClient,
    IUserHttpClient userHttpClient) : IEmployeeService
{
    public async Task<Result<EmployeeResponse>> GetSelf()
    {
        var result = await employeeHttpClient.GetSelfAsync();
        return result.Map();
    }

    public async Task<Result<EmployeeAddressResponse>> UpdateEmployeeAddress(EmployeeAddressUpdateRequest request)
    {
        var result = await employeeHttpClient.UpdateEmployeeAddressAsync(request);
        return result.Map();
    }

    public async Task<Result> ChangePassword(PasswordChangeRequest request)
    {
        var result = await userHttpClient.ChangePasswordAsync(request);
        return result.Map();
    
    }
}