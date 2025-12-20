using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController 
    : CrudControllerBase<Guid, EmployeeAddRequest, EmployeeUpdateRequest, EmployeeResponse>
{
    private readonly IEmployeeService _employeeService;
    public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        => _employeeService = employeeService;
    
    [HttpPost]
    //[HasPermission("employee:create")]
    public override Task<ActionResult<CreatedResponse>> PostAsync(EmployeeAddRequest request)
        => base.PostAsync(request);

    [HttpGet("{id}")]
    //[HasPermission("employee:read")]
    public override Task<ActionResult<EmployeeResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => base.GetByIdAsync(id, cancellationToken);

    [HttpPut("{id}")]
    //[HasPermission("employee:update")]
    public override Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, EmployeeUpdateRequest request) 
        => base.PutAsync(id, request);

    [HttpDelete("{id}")]
    //[HasPermission("employee:delete")]
    public override Task<IActionResult> DeleteAsync(Guid id)
        => base.DeleteAsync(id);

    [HttpPut("address")]
    public async Task<ActionResult<UpdatedResponse>> PutEmployeeAddress(Guid employeeId, EmployeeAddressUpdateRequest request)
        => HandleResult(await _employeeService.UpdateEmployeeAddressAsync(CurrentUserId, request));

    [HttpGet("list")]
    public async Task<ActionResult<EmployeeListResponse>> GetEmployeesList(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeesListAsync(ct));

    [HttpGet("me")]
    //[HasPermission("employee:read:me")]
    public async Task<ActionResult<EmployeeProfileResponse>> GetEmployee(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeeProfileInfoAsync(CurrentUserId, ct));
}
