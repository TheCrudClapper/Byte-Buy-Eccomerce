using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeHttpClient _employeeHttpClient;

    public EmployeeService(IEmployeeHttpClient employeeHttpClient)
    {
        _employeeHttpClient = employeeHttpClient;
    }
    
    public async Task<Result<EmployeeResponse>> GetSelf()
    {
        var result = await _employeeHttpClient.GetSelfAsync();

        if (!result.Success)
            return Result<EmployeeResponse>.Fail(result.Error!);

        return Result<EmployeeResponse>.Ok(result.Value!);
    }
}