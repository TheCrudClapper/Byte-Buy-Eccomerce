using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Employee;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Employee;
using ByteBuy.Core.Pagination;
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
    public async Task<ActionResult<UpdatedResponse>> PutEmployeeAddressAsync(
        EmployeeAddressUpdateRequest request)
        => HandleResult(await _employeeService.UpdateEmployeeAddressAsync(CurrentUserId, request));

    [HttpGet("list")]
    public async Task<ActionResult<PagedList<EmployeeListResponse>>> GetEmployeesListAsync(
        [FromQuery] EmployeeListQuery queryParams, CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeesListAsync(CurrentUserId, queryParams, ct));

    [HttpGet("me")]
    //[HasPermission("employee:read:me")]
    public async Task<ActionResult<EmployeeProfileResponse>> GetMyEmployeeAsync(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeeProfileInfoAsync(CurrentUserId, ct));
}
