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
    public async Task<ActionResult<EmployeeResponse>> PostEmployee(EmployeeAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _employeeService.AddEmployee(request, cancellationToken));

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken cancellationToken)
        => HandleResult(await _employeeService.GetEmployee(employeeId, cancellationToken));

    [HttpGet]
    public async Task<ActionResult<EmployeeResponse>> GetEmployees()
        => HandleResult(await _employeeService.GetEmployees());

    [HttpPut("{employeeId}")]
    public async Task<ActionResult<EmployeeResponse>> PutEmployee(
    Guid employeeId,
    EmployeeUpdateRequest request,
    CancellationToken cancellationToken)
        => HandleResult(await _employeeService.UpdateEmployee(employeeId, request, cancellationToken));

}
