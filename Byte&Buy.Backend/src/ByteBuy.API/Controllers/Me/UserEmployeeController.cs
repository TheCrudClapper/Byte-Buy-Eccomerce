using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Employee;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Route("api/me/employee")]
[ApiController]
public class UserEmployeeController : BaseApiController
{
    private readonly IEmployeeService _employeeService;
    public UserEmployeeController(IEmployeeService employeeService)
        => _employeeService = employeeService;

    [HttpGet]
    public async Task<ActionResult<EmployeeProfileResponse>> GetEmployeeProfileData(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeeProfileInfoAsync(CurrentUserId, ct));
}
