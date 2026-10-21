using Microsoft.AspNetCore.Authorization;

namespace ByteBuy.API.Controllers.Public;

[Route("api/conditions")]
[ApiController]
[AllowAnonymous]
public class PublicConditionsController : BaseApiController
{
    private readonly IConditionService _conditionService;
    public PublicConditionsController(IConditionService conditionService)
        => _conditionService = conditionService;

    [HttpGet("options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
      => HandleResult(await _conditionService.GetSelectListAsync(ct));
}
