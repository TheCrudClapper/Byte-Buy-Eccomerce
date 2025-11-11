using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public Task<Result<EmployeeResponse>> AddEmployee(EmployeeAddRequest request, CancellationToken cancelationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken cancelationToken)
    {
        var employee = await _employeeRepository.GetAsync(employeeId, cancelationToken);

        if (employee is null)
            return Result.Failure<EmployeeResponse>(Error.NotFound);

        return employee.ToEmployeeResponse();
    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetEmployees(CancellationToken cancelationToken)
    {
        var employees = await _employeeRepository.GetAllAsync(cancelationToken);

        return employees.Select(e => e.ToEmployeeResponse())
            .ToList();
    }

    public Task<Result<EmployeeResponse>> UpdateEmployee(Guid employeeId, EmployeeUpdateRequest request, CancellationToken cancelationToken)
    {
        throw new NotImplementedException();
    }
}

