using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Roles;
using ByteBuy.Core.Domain.Roles.Errors;
using ByteBuy.Core.Domain.Shared.DomainServicesContracts;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Users;
using ByteBuy.Core.Domain.Users.Base;
using ByteBuy.Core.Domain.Users.Errors;
using ByteBuy.Core.DTO.Public.Employee;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.Filtration.Employee;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using static ByteBuy.Core.Specification.EmployeeSpecifications;

namespace ByteBuy.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordService _passwordService;
    private readonly ICompanyRepository _companyRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IAddressValidationService _addressValidator;

    public EmployeeService(
        IUserRepository applicationUserRepository,
        IEmployeeRepository employeeRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IPasswordService passwordService,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork,
        IAddressValidationService addressValidator)
    {
        _userRepository = applicationUserRepository;
        _roleManager = roleManager;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _passwordService = passwordService;
        _addressValidator = addressValidator;
        _companyRepository = companyRepository;
    }

    public async Task<Result<CreatedResponse>> AddAsync(EmployeeAddRequest request)
    {
        if (await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<CreatedResponse>(AuthErrors.EmailAlreadyTaken);

        var applicationRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (applicationRole is null)
            return Result.Failure<CreatedResponse>(RoleErrors.NotFound);

        var company = await _companyRepository.GetAsync();
        if (company is null)
            return Result.Failure<CreatedResponse>(EmployeeErrors.CompanyNotFound);

        var employeeResult = Employee.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.HomeAddress.Street,
            request.HomeAddress.HouseNumber,
            request.HomeAddress.PostalCity,
            request.HomeAddress.PostalCode,
            request.HomeAddress.City,
            request.HomeAddress.Country,
            request.HomeAddress.FlatNumber,
            request.PhoneNumber,
            company.Id,
            request.RevokedPermissionIds,
            request.GrantedPermissionIds,
            _addressValidator
            );

        if (employeeResult.IsFailure)
            return Result.Failure<CreatedResponse>(employeeResult.Error);

        var employee = employeeResult.Value;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var identityResult = await _userManager.CreateAsync(employee, request.Password);

            if (!identityResult.Succeeded)
                return identityResult.ToResult<CreatedResponse>();

            var roleResult = await _userManager.AddToRoleAsync(employee, applicationRole.Name!);
            if (!roleResult.Succeeded)
                return roleResult.ToResult<CreatedResponse>();

            await _unitOfWork.CommitAsync();

            return employee.ToCreatedResponse();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            return Result.Failure<CreatedResponse>(EmployeeErrors.EmployeeCreationFailed);
        }

    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, EmployeeUpdateRequest request)
    {
        var employee = await _employeeRepository.GetBySpecAsync(new EmployeeAggregateSpec(id));
        if (employee is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var newRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (newRole is null)
            return Result.Failure<UpdatedResponse>(RoleErrors.NotFound);

        if (employee.Email != request.Email && await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<UpdatedResponse>(AuthErrors.EmailAlreadyTaken);

        var updateResult = employee.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.HomeAddress.Street,
            request.HomeAddress.HouseNumber,
            request.HomeAddress.PostalCity,
            request.HomeAddress.PostalCode,
            request.HomeAddress.City,
            request.HomeAddress.Country,
            request.HomeAddress.FlatNumber,
            request.PhoneNumber,
            request.GrantedPermissionIds,
            request.RevokedPermissionIds,
            _addressValidator);

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var validation = await _passwordService.ValidateAsync(employee, request.Password);
                if (!validation.Succeeded)
                    return validation.ToResult<UpdatedResponse>();

                var change = await _passwordService.ChangePasswordAsync(employee, request.Password);
                if (!change.Succeeded)
                    return change.ToResult<UpdatedResponse>();
            }

            var roleChange = await UpdateEmployeeRoleAsync(employee, newRole);
            if (roleChange.IsFailure)
                return Result.Failure<UpdatedResponse>(roleChange.Error);

            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            return employee.ToUpdatedResponse();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            return Result.Failure<UpdatedResponse>(EmployeeErrors.EmployeeUpdateFailed);
        }

    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee is null)
            return Result.Failure(Error.NotFound);

        employee.Deactivate();

        await _employeeRepository.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<EmployeeResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var employeeDto = await _employeeRepository
           .GetBySpecAsync(new EmployeeResponseSpec(id), ct);

        return employeeDto is null
            ? Result.Failure<EmployeeResponse>(Error.NotFound)
            : employeeDto;
    }

    public async Task<Result<EmployeeProfileResponse>> GetEmployeeProfileInfoAsync(
        Guid employeeId, CancellationToken ct = default)
    {
        var employeeDto = await _employeeRepository
            .GetBySpecAsync(new EmployeeProfileResponseSpec(employeeId), ct);
        if (employeeDto is null)
            return Result.Failure<EmployeeProfileResponse>(Error.NotFound);

        return employeeDto;
    }

    /// <summary>
    /// Get list of all employees except current logged user
    /// </summary>
    /// <param name="excludedUserId">User SellerId corresponding to current user</param>
    /// <param name="ct">Cancelation for stopping async operations</param>
    /// <returns>A Dto list of employees within company</returns>
    public async Task<Result<PagedList<EmployeeListResponse>>> GetEmployeesListAsync(
        Guid excludedUserId, EmployeeListQuery queryParams, CancellationToken ct = default)
    {
        return await _employeeRepository.GetEmployeePagedListAsync(excludedUserId, queryParams, ct);
    }


    public async Task<Result<UpdatedResponse>> UpdateEmployeeAddressAsync(
        Guid employeeId, EmployeeAddressUpdateRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var updateResult = employee.ChangeHomeAddress(
                                request.HomeAddress.Street,
                                request.HomeAddress.HouseNumber,
                                request.HomeAddress.PostalCity,
                                request.HomeAddress.PostalCode,
                                request.HomeAddress.City,
                                request.HomeAddress.Country,
                                request.HomeAddress.FlatNumber,
                                request.PhoneNumber,
                                _addressValidator);

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _employeeRepository.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return employee.ToUpdatedResponse();
    }

    private async Task<Result> UpdateEmployeeRoleAsync(ApplicationUser employee, ApplicationRole newRole)
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

