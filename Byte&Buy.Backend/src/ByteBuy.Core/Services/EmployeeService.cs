using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
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
    private readonly IPasswordService _passwordService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public EmployeeService(
        IUserRepository applicationUserRepository,
        IEmployeeRepository employeeRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IPasswordService passwordService)
    {
        _userRepository = applicationUserRepository;
        _roleManager = roleManager;
        _userManager = userManager;
        _employeeRepository = employeeRepository;
        _passwordService = passwordService;
    }
    public async Task<Result<CreatedResponse>> AddEmployee(EmployeeAddRequest request)
    {
        if (await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<CreatedResponse>(AuthErrors.AccountExists);

        var applicationRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (applicationRole is null)
            return Result.Failure<CreatedResponse>(RoleErrors.NotFound);

        var employeeResult = Employee.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Street,
            request.HouseNumber,
            request.PostalCode,
            request.City,
            request.Country,
            request.FlatNumber,
            request.PhoneNumber);

        if (employeeResult.IsFailure)
            return Result.Failure<CreatedResponse>(employeeResult.Error);

        var employee = employeeResult.Value;

        var identityResult = await _userManager.CreateAsync(employee, request.Password);

        if (!identityResult.Succeeded)
            return identityResult.ToResult<CreatedResponse>();

        var roleResult = await _userManager.AddToRoleAsync(employee, applicationRole.Name!);
        if (!roleResult.Succeeded)
            return roleResult.ToResult<CreatedResponse>();

        return employee.ToCreatedResponse();
    }

    public async Task<Result> DeleteEmployee(Guid employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);

        if (employee is null)
            return Result.Failure(Error.NotFound);

        employee.Deactivate();

        await _employeeRepository.UpdateAsync(employee);

        return Result.Success();
    }

    public async Task<Result<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken ct)
    {
        var employee = await _employeeRepository.GetWithRolesById(employeeId, ct);

        if (employee is null)
            return Result.Failure<EmployeeResponse>(Error.NotFound);

        return employee.ToEmployeeResponse();
    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetEmployees(CancellationToken ct)
    {
        var employees = await _employeeRepository.GetAllWithRolesAsync(ct);

        return employees.Select(e => e.ToEmployeeResponse())
            .ToList();
    }

    public async Task<Result<IEnumerable<EmployeeListResponse>>> GetEmployeesList(CancellationToken ct = default)
    {
        var employees = await _employeeRepository.GetAllWithRolesAsync(ct);
        return employees
            .Select(e => e.ToEmployeeListResponse()).ToList();
    }

    public async Task<Result<UpdatedResponse>> UpdateEmployee(Guid employeeId, EmployeeUpdateRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var newRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (newRole is null)
            return Result.Failure<UpdatedResponse>(RoleErrors.NotFound);

        if (employee.Email != request.Email && await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<UpdatedResponse>(AuthErrors.AccountExists);

        var updateResult = employee.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Street,
            request.HouseNumber,
            request.PostalCode,
            request.City,
            request.Country,
            request.FlatNumber,
            request.PhoneNumber);

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var validation = await _passwordService.ValdiateAsync(employee, request.Password);
            if (!validation.Succeeded)
                return validation.ToResult<UpdatedResponse>();

            var change = await _passwordService.ChangePasswordAsync(employee, request.Password);
            if (!validation.Succeeded)
                return change.ToResult<UpdatedResponse>();
        }

        var roleChange = await UpdateEmployeeRole(employee, newRole);
        if (roleChange.IsFailure)
            return Result.Failure<UpdatedResponse>(roleChange.Error);

        await _employeeRepository.UpdateAsync(employee);

        return employee.ToUpdatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateEmployeeAddress(Guid employeeId, EmployeeAddressUpdateRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var updateResult = employee.ChangeAddress(
                                request.Street,
                                request.HouseNumber,
                                request.PostalCode,
                                request.City,
                                request.Country,
                                request.FlatNumber,
                                request.PhoneNumber);

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _employeeRepository.UpdateAsync(employee);
        return employee.ToUpdatedResponse();
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

