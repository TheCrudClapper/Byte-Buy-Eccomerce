using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : BaseApiController
{
    private readonly IEmployeeService _employeeService;
    public EmployeesController(IEmployeeService employeeService)
        => _employeeService = employeeService;

    [HttpPost]
    //[HasPermission("employee:create")]
    public async Task<ActionResult<EmployeeResponse>> PostEmployee(EmployeeAddRequest request)
        => HandleResult(await _employeeService.AddEmployee(request));

    [HttpGet("{employeeId}")]
    //[HasPermission("employee:read")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployee(employeeId, ct));

    [HttpPut("address")]
    public async Task<ActionResult<EmployeeAddressResponse>> PutEmployeeAddress(Guid employeeId, EmployeeAddressUpdateRequest request)
        => HandleResult(await _employeeService.UpdateEmployeeAddress(CurrentUserId, request));

    [HttpGet("me")]
    //[HasPermission("employee:read:self")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployee(CurrentUserId, ct));

    [HttpGet]
    //[HasPermission("employee:read:many")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployees(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployees(ct));

    [HttpPut("{employeeId}")]
    //[HasPermission("employee:update")]
    public async Task<ActionResult<EmployeeResponse>> PutEmployee(
    Guid employeeId,
    EmployeeUpdateRequest request)
        => HandleResult(await _employeeService.UpdateEmployee(employeeId, request));

    [HttpDelete("{employeeId}")]
    //[HasPermission("employee:delete")]
    public async Task<IActionResult> DeleteEmployee(Guid employeeId)
        => HandleResult(await _employeeService.DeleteEmployee(employeeId));
   
}
