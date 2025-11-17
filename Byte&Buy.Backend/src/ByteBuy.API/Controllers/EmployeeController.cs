using ByteBuy.API.Attributes;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class EmployeeController : BaseApiController
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    [HasPermission("employee:create")]
    public async Task<ActionResult<EmployeeResponse>> PostEmployee(EmployeeAddRequest request, CancellationToken ct)
        => HandleResult(await _employeeService.AddEmployee(request, ct));

    [HttpGet("{employeeId}")]
    [HasPermission("employee:read")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployee(employeeId, ct));

    [HttpGet]
    [HasPermission("employee:read:many")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployees()
        => HandleResult(await _employeeService.GetEmployees());

    [HttpPut("{employeeId}")]
    [HasPermission("employee:update")]
    public async Task<ActionResult<EmployeeResponse>> PutEmployee(
    Guid employeeId,
    EmployeeUpdateRequest request,
    CancellationToken ct)
        => HandleResult(await _employeeService.UpdateEmployee(employeeId, request, ct));

    [HttpDelete("{employeeId}")]
    [HasPermission("employee:delete")]
    public async Task<IActionResult> DeleteEmployee(Guid employeeId, CancellationToken ct)
        => HandleResult(await _employeeService.DeleteEmployee(employeeId, ct));
   
}
