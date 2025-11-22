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
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public EmployeeService(
        IUserRepository applicationUserRepository,
        IEmployeeRepository employeeRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userRepository = applicationUserRepository;
        _roleManager = roleManager;
        _userManager = userManager;
        _employeeRepository = employeeRepository;
    }
    public async Task<Result<EmployeeResponse>> AddEmployee(EmployeeAddRequest request)
    {
        if (await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<EmployeeResponse>(AuthErrors.AccountExists);

        var applicationRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (applicationRole is null)
            return Result.Failure<EmployeeResponse>(RoleErrors.NotFound);

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

        var roleResult = await _userManager.AddToRoleAsync(employee, applicationRole.Name!);
        if(!roleResult.Succeeded)
            return roleResult.ToResult<EmployeeResponse>();

        return employee.ToEmployeeResponse();
    }

    public async Task<Result> DeleteEmployee(Guid employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);

        if (employee is null)
            return Result.Failure(Error.NotFound);

        employee.Deactivate();

        await _employeeRepository.SoftDeleteAsync(employee);

        return Result.Success();
    }

    public async Task<Result<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken ct)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId, ct);

        if (employee is null)
            return Result.Failure<EmployeeResponse>(Error.NotFound);

        return employee.ToEmployeeResponse();
    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetEmployees(CancellationToken ct)
    {
        var employees = await _employeeRepository.GetAllAsync(ct);

        return employees.Select(e => e.ToEmployeeResponse())
            .ToList();
    }

    public async Task<Result<EmployeeResponse>> UpdateEmployee(Guid employeeId, EmployeeUpdateRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee is null)
            return Result.Failure<EmployeeResponse>(Error.NotFound);

        var newRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (newRole is null)
            return Result.Failure<EmployeeResponse>(RoleErrors.NotFound);

        if (employee.Email != request.Email && await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<EmployeeResponse>(AuthErrors.AccountExists);

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

        var roleChange = await UpdateEmployeeRole(employee, newRole);
        if (roleChange.IsFailure)
            return Result.Failure<EmployeeResponse>(roleChange.Error);

        await _employeeRepository.UpdateAsync(employee);

        return employee.ToEmployeeResponse();
    }

    private async Task<Result> UpdateEmployeeRole(ApplicationUser employee, ApplicationRole newRole)
    {
        var currentRoles = await _userManager.GetRolesAsync(employee);
        var currentRole = currentRoles.SingleOrDefault();

        if (currentRole == newRole.Name)
            return Result.Success();

        if (currentRole is not null)
        {
            var remove = await _userManager.RemoveFromRoleAsync(employee, currentRole);
            if (!remove.Succeeded)
                return remove.ToResult();
        }

        var add = await _userManager.AddToRoleAsync(employee, newRole.Name!);
        if (!add.Succeeded)
            return add.ToResult();

        return Result.Success();
    }
}

