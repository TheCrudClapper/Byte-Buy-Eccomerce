using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : BaseApiController
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeResponse>> PostEmployee(EmployeeAddRequest request, CancellationToken ct)
        => HandleResult(await _employeeService.AddEmployee(request, ct));

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployee(employeeId, ct));

    [HttpGet]
    public async Task<ActionResult<EmployeeResponse>> GetEmployees()
        => HandleResult(await _employeeService.GetEmployees());

    [HttpPut("{employeeId}")]
    public async Task<ActionResult<EmployeeResponse>> PutEmployee(
    Guid employeeId,
    EmployeeUpdateRequest request,
    CancellationToken ct)
        => HandleResult(await _employeeService.UpdateEmployee(employeeId, request, ct));

    [HttpDelete("{employeeId}")]
    public async Task<IActionResult> DeleteEmployee(Guid employeeId, CancellationToken ct)
        => HandleResult(await _employeeService.DeleteEmployee(employeeId, ct));
   
}
