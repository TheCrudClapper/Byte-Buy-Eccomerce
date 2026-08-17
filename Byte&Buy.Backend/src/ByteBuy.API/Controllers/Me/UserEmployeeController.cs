using ByteBuy.Core.DTO.Public.Employee;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-employee")]
[Route("api/me/employee")]
[ApiController]
public class UserEmployeeController : BaseApiController
{
    private readonly IEmployeeService _employeeService;
    public UserEmployeeController(IEmployeeService employeeService)
        => _employeeService = employeeService;

    [HttpGet]
    [HasPermission("{resource}:read:one")]
    public async Task<ActionResult<EmployeeProfileResponse>> GetEmployeeProfileDataAsync(CancellationToken ct)
        => HandleResult(await _employeeService.GetEmployeeProfileInfoAsync(CurrentUserId, ct));
}
