using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Employee;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Employee;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-employees")]
[ApiController]
[Route("api/company/employees")]
public class CompanyEmployeesController
    : CrudControllerBase<Guid, EmployeeAddRequest, EmployeeUpdateRequest, EmployeeResponse>
{
    private readonly IEmployeeService _employeeService;
    public CompanyEmployeesController(IEmployeeService employeeService) : base(employeeService)
        => _employeeService = employeeService;

    [HttpPut("address")]
    [HasPermission("company-employees:update:address")]
    public async Task<ActionResult<UpdatedResponse>> PutEmployeeAddressAsync(
        EmployeeAddressUpdateRequest request)
        => HandleResult(await _employeeService.UpdateEmployeeAddressAsync(CurrentUserId, request));

    [HttpGet("list")]
    [HasPermission("company-employees:read:many")]
    public async Task<ActionResult<PagedList<EmployeeListResponse>>> GetEmployeesListAsync(
        [FromQuery] EmployeeListQuery queryParams, CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeesListAsync(CurrentUserId, queryParams, ct));
}
