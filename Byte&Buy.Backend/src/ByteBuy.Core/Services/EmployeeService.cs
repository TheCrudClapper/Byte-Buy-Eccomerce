using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public EmployeeService(IEmployeeRepository employeeRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _employeeRepository = employeeRepository;
    }
    public async Task<Result<EmployeeResponse>> AddEmployee(EmployeeAddRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<EmployeeResponse>(AuthErrors.AccountExists);

        var employeeResult = Employee.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Street,
            request.HouseNumber,
            request.PostalCode,
            request.City,
            request.Country,
            request.FlatNumber
            );

        if (employeeResult.IsFailure)
            return Result.Failure<EmployeeResponse>(employeeResult.Error);

        var employee = employeeResult.Value;

        var identityResult = await _userManager.CreateAsync(employee, request.Password);

        if (!identityResult.Succeeded)
            return identityResult.ToResult<EmployeeResponse>();

        return employee.ToEmployeeResponse();
    }

    public async Task<Result> DeleteEmployee(Guid employeeId, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetAsync(employeeId, cancellationToken);

        if (employee is null)
            return Result.Failure(Error.NotFound);

        employee.Deactivate();

        await _employeeRepository.DeleteAsync(employee, cancellationToken);

        return Result.Success();
    }

    public async Task<Result<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetAsync(employeeId, cancellationToken);

        if (employee is null)
            return Result.Failure<EmployeeResponse>(Error.NotFound);

        return employee.ToEmployeeResponse();
    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetEmployees(CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAllAsync(cancellationToken);

        return employees.Select(e => e.ToEmployeeResponse())
            .ToList();
    }

    public async Task<Result<EmployeeResponse>> UpdateEmployee(Guid employeeId, EmployeeUpdateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetAsync(employeeId, cancellationToken);
        if (employee is null)
            return Result.Failure<EmployeeResponse>(Error.NotFound);

        var updateResult = employee.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Street,
            request.HouseNumber,
            request.PostalCode,
            request.City,
            request.Country,
            request.FlatNumber);

        if (updateResult.IsFailure)
            return Result.Failure<EmployeeResponse>(updateResult.Error);

        var updatedEmployee = await _employeeRepository.UpdateAsync(employee, cancellationToken);

        return Result.Success(updatedEmployee.ToEmployeeResponse());
    }
}

