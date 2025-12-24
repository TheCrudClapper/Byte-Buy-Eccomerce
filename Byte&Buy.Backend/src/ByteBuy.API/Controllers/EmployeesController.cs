using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("employees")]
[ApiController]
[Route("api/[controller]")]
public class EmployeesController
    : CrudControllerBase<Guid, EmployeeAddRequest, EmployeeUpdateRequest, EmployeeResponse>
{
    private readonly IEmployeeService _employeeService;
    public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        => _employeeService = employeeService;

    [HttpPut("address")]
    public async Task<ActionResult<UpdatedResponse>> PutEmployeeAddress(Guid employeeId, EmployeeAddressUpdateRequest request)
        => HandleResult(await _employeeService.UpdateEmployeeAddressAsync(CurrentUserId, request));

    [HttpGet("list")]
    public async Task<ActionResult<EmployeeListResponse>> GetEmployeesList(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeesListAsync(CurrentUserId,ct));

    [HttpGet("me")]
    //[HasPermission("employee:read:me")]
    public async Task<ActionResult<EmployeeProfileResponse>> GetEmployee(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeeProfileInfoAsync(CurrentUserId, ct));
}
